using AutoMapper;
using MediatR;
using Wash_Wow.Application.Common.Interfaces;
using WashAndWow.Domain.Entities;
using WashAndWow.Domain.Repositories;
using static Wash_Wow.Domain.Enums.Enums;
using WashAndWow.Infrastructure.Repositories;
using Wash_Wow.Domain.Common.Exceptions;
using Wash_Wow.Domain.Repositories;

namespace WashAndWow.Application.Rating.Create
{
    public class CreateRatingCommandHandler : IRequestHandler<CreateRatingCommand, RatingDto>
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly INotificationRepository _notificationRepository;
        private readonly ILaundryShopRepository _laundryShopRepository;
        private readonly IUserRepository _userRepository;

        public CreateRatingCommandHandler(
            IRatingRepository ratingRepository, 
            IMapper mapper,
            ICurrentUserService currentUserService,
            INotificationRepository notificationRepository,
            ILaundryShopRepository laundryShopRepository,
            IUserRepository userRepository)
        {
            _ratingRepository = ratingRepository;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _notificationRepository = notificationRepository;
            _laundryShopRepository = laundryShopRepository;
            _userRepository = userRepository;
        }

        public async Task<RatingDto> Handle(CreateRatingCommand request, CancellationToken cancellationToken)
        {
            // Retrieve LaundryShop to get OwnerID and Name
            var laundryShop = await _laundryShopRepository.FindAsync(
                x => x.ID == request.LaundryShopID && x.DeletedAt == null, cancellationToken);

            if (laundryShop == null)
            {
                throw new NotFoundException("Laundry Shop not found.");
            }
            var user = await _userRepository.FindAsync(x => x.ID == request.UserId);
            if (user == null)
            {
                throw new NotFoundException("User not found.");
            }
            // Create rating entity
            var ratingEntity = new RatingEntity
            {
                RatingValue = request.RatingValue,
                Comment = request.Comment,
                LaundryShopID = request.LaundryShopID,
                CreatorID = request.UserId
            };

            _ratingRepository.Add(ratingEntity);

            // Save rating to generate the ID
            await _ratingRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            // Create and save notification
            var notification = new NotificationEntity
            {
                ReceiverID = laundryShop.ID,
                Content = $"A new rating (ID: {ratingEntity.ID}) has been created for Laundry Shop: {laundryShop.Name} by User {user.FullName} .",
                IsRead = false,
                CreatedAt = DateTime.UtcNow,
                CreatorID = _currentUserService.UserId,
                Type = NotificationType.RatingCreated
            };

            _notificationRepository.Add(notification);

            // Save notification
            await _notificationRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return ratingEntity.MapToRatingDto(_mapper, user.FullName);
        }
    }
}

