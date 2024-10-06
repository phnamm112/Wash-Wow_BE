using AutoMapper;
using MediatR;
using Wash_Wow.Application.Common.Interfaces;
using Wash_Wow.Domain.Common.Exceptions;
using Wash_Wow.Domain.Entities;
using Wash_Wow.Domain.Repositories;
using WashAndWow.Domain.Entities;
using WashAndWow.Domain.Repositories;
using static Wash_Wow.Domain.Enums.Enums;

namespace WashAndWow.Application.Booking.Create
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, string>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;
        private readonly IBookingItemRepository _bookingItemRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILaundryShopRepository _laundryShopRepository;
        private readonly IShopServiceRepository _shopServiceRepository;
        private readonly IVoucherRepository _voucherRepository;
        private readonly IUserRepository _userRepository;

        public CreateBookingCommandHandler(IBookingRepository bookingRepository
            , IMapper mapper
            , IBookingItemRepository bookingItemRepository
            , ICurrentUserService currentUserService
            , ILaundryShopRepository laundryShopRepository
            , IShopServiceRepository shopServiceRepository
            , IVoucherRepository voucherRepository
            , IUserRepository userRepository)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
            _bookingItemRepository = bookingItemRepository;
            _currentUserService = currentUserService;
            _laundryShopRepository = laundryShopRepository;
            _shopServiceRepository = shopServiceRepository;
            _voucherRepository = voucherRepository;
            _userRepository = userRepository;
        }

        public async Task<string> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
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
            if (request.VoucherId != null && (voucher == null || voucher.ExpiryDate > DateTime.Now))
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
                BookingItemEntity bookingItem = new BookingItemEntity
                {
                    BookingID = booking.ID,
                    ServicesID = shopService.ID,
                    CreatedAt = DateTime.UtcNow,
                    CreatorID = _currentUserService.UserId,
                };
                _bookingItemRepository.Add(bookingItem);
                booking.BookingItems.Add(bookingItem);

                // Calculate total price of booking
                booking.TotalPrice += shopService.PricePerKg * booking.TotalPrice;
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
                            break;
                        }
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                _userRepository.Update(user);
                _voucherRepository.Update(voucher);
            }

            _bookingRepository.Update(booking);
            return await _bookingRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Success" : "Failed";
        }
    }
}
