using AutoMapper;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using Wash_Wow.Application.Common.Interfaces;
using Wash_Wow.Domain.Common.Exceptions;
using Wash_Wow.Domain.Entities;
using Wash_Wow.Domain.Repositories;
using WashAndWow.Domain.Entities;
using WashAndWow.Domain.Entities.ConfigTable;
using WashAndWow.Domain.Repositories;
using static Wash_Wow.Domain.Enums.Enums;

namespace WashAndWow.Application.Booking.Create
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, CreateBookingResponse>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;
        private readonly IBookingItemRepository _bookingItemRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILaundryShopRepository _laundryShopRepository;
        private readonly IShopServiceRepository _shopServiceRepository;
        private readonly IVoucherRepository _voucherRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly INotificationRepository _notificationRepository;

        public CreateBookingCommandHandler(IBookingRepository bookingRepository
            , IMapper mapper
            , IBookingItemRepository bookingItemRepository
            , ICurrentUserService currentUserService
            , ILaundryShopRepository laundryShopRepository
            , IShopServiceRepository shopServiceRepository
            , IVoucherRepository voucherRepository
            , IUserRepository userRepository
            , IPaymentRepository paymentRepository
            , INotificationRepository notificationRepository)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
            _bookingItemRepository = bookingItemRepository;
            _currentUserService = currentUserService;
            _laundryShopRepository = laundryShopRepository;
            _shopServiceRepository = shopServiceRepository;
            _voucherRepository = voucherRepository;
            _userRepository = userRepository;
            _paymentRepository = paymentRepository;
            _notificationRepository = notificationRepository;
        }

        public async Task<CreateBookingResponse> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            // User validation
            var user = await _userRepository.FindAsync(x => x.ID == _currentUserService.UserId && x.DeletedAt == null, cancellationToken);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            // LaundryShop validation
            var laundryShop = await _laundryShopRepository.FindAsync(x => x.ID == request.LaundryShopId && x.DeletedAt == null, cancellationToken);
            if (laundryShop == null)
            {
                throw new NotFoundException("Laundry shop is not exist");
            }

            // Voucher validation
            var voucher = await _voucherRepository.FindAsync(x => x.ID == request.VoucherId && x.DeletedAt == null, cancellationToken);
            if (!request.VoucherId.IsNullOrEmpty() && (voucher == null || voucher.ExpiryDate < DateTime.Now))
            {
                throw new NotFoundException("Voucher is not exist");
            }
            if (voucher != null && voucher.Users.Contains(user))
            {
                throw new Exception("Voucher has been used");
            }
            
            // Intialize booking
            BookingEntity booking = new BookingEntity
            {
                Status = BookingStatus.PENDING,
                ShopPickupTime = request.ShopPickupTime,
                CustomerPickpupTime = request.CustomerPickupTime,
                Note = request.Note ?? user.FullName, // điền note hoặc note là tên người dùng
                TotalPrice = 0,
                LaundryShopID = laundryShop.ID,
                LaundryWeight = request.LaundryWeight,
                CreatedAt = DateTime.UtcNow,
                CreatorID = user.ID,
            };
            _bookingRepository.Add(booking);

            // BookingItem validation
            List<BookingItemEntity> bookingItems = new List<BookingItemEntity>();
            foreach (var item in request.BookingItems)
            {
                var shopService = await _shopServiceRepository.FindAsync(x => x.ID == item.ServicesId && x.DeletedAt == null, cancellationToken);
                if (shopService == null)
                {
                    throw new NotFoundException("Shop service is not exist");
                }
                if (shopService.ShopID != booking.LaundryShopID)
                {
                    throw new Exception("Shop service is not belong to LaundryShopID: " + booking.LaundryShopID);
                }
                // Check if the laundry weight meets the minimum requirement for the service
                if ((decimal)booking.LaundryWeight < shopService.MinLaundryWeight)
                {
                    throw new Exception($"Laundry weight must be at least {shopService.MinLaundryWeight} kg for service {shopService.Name}");
                }
                BookingItemEntity bookingItem = new BookingItemEntity
                {
                    BookingID = booking.ID,
                    ServicesID = shopService.ID,
                    Amount = shopService.PricePerKg * (decimal)booking.LaundryWeight,
                    CreatedAt = DateTime.UtcNow,
                    CreatorID = _currentUserService.UserId,
                };
                _bookingItemRepository.Add(bookingItem);
                booking.BookingItems.Add(bookingItem);

                // Calculate total price of booking
                booking.TotalPrice += bookingItem.Amount;
            }
            // Update that user has used voucher
            if (voucher != null)
            {
                voucher.Users.Add(user);
                user.UsedVouchers.Add(voucher);
                booking.VoucherID = voucher.ID;
                if (voucher.ConditionOfUse > booking.TotalPrice)
                {
                    throw new Exception("Invalid condition, total price must be greater than condition of use");
                }

                switch (voucher.Type)
                {
                    case VoucherType.DISCOUNT_BY_AMOUNT:
                        {
                            decimal amountToReduce = voucher.Amount;
                            booking.TotalPrice -= amountToReduce;
                            booking.VoucherDiscounted = amountToReduce;
                            break;
                        }
                    case VoucherType.DISCOUNT_BY_PERCENT:
                        {
                            decimal percentDiscount = voucher.Amount / 100m;
                            decimal amountToReduce = booking.TotalPrice * percentDiscount;

                            if (voucher.MaximumReduce.HasValue)
                            {
                                amountToReduce = Math.Min(amountToReduce, voucher.MaximumReduce.Value);
                            }
                            if (voucher.MinimumReduce.HasValue)
                            {
                                amountToReduce = Math.Max(amountToReduce, voucher.MinimumReduce.Value);
                            }

                            booking.TotalPrice -= amountToReduce;
                            booking.VoucherDiscounted = amountToReduce;
                            break;
                        }
                    default:
                        throw new ArgumentOutOfRangeException();
                }             
                _userRepository.Update(user);
                _voucherRepository.Update(voucher);
            }
            // ADD payment
            var payment = new PaymentEntity
            {
                Amount = (double)booking.TotalPrice,
                BookingID = booking.ID,
                Name = booking.ID + " BANKING",
                Status = PaymentStatus.PENDING,
                ExpiryTime = booking.ShopPickupTime.AddHours(-1),
                CreatedAt = DateTime.UtcNow,
                CreatorID = _currentUserService.UserId,
            };
            _paymentRepository.Add(payment);

            // Create and save notification
            var notification = new NotificationEntity
            {
                ReceiverID = laundryShop.ID,
                Content = $"A new booking has been created with ID: {booking.ID} for Laundry Shop: {laundryShop.Name}.",
                IsRead = false,
                CreatedAt = DateTime.UtcNow,
                CreatorID = _currentUserService.UserId,
                Type = NotificationType.BookingCreated 
            };
            _notificationRepository.Add(notification);
            var success = await _bookingRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0;
            if (!success)
                throw new Exception("Failed to create booking or payment.");
            return new CreateBookingResponse(booking.ID, payment.ID);
        }
    }
}
