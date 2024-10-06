using AutoMapper;
using MediatR;
using Wash_Wow.Domain.Common.Exceptions;
using Wash_Wow.Domain.Repositories;
using WashAndWow.Domain.Repositories;

namespace WashAndWow.Application.LaundryShops.Read
{
    public class GetLaundryShopByIdQueryHandler : IRequestHandler<GetLaundryShopByIdQuery, LaundryShopDto>
    {
        private readonly ILaundryShopRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetLaundryShopByIdQueryHandler(
            ILaundryShopRepository repository,
            IUserRepository userRepository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<LaundryShopDto> Handle(GetLaundryShopByIdQuery request, CancellationToken cancellationToken)
        {
            var shop = await _repository.FindAsync(x => x.ID == request.Id && x.DeletedAt == null, cancellationToken);

            if (shop == null)
            {
                throw new NotFoundException("Laundry shop is not exist");
            }
            var user = await _userRepository.FindAsync(x => x.ID == shop.OwnerID && x.DeletedAt == null, cancellationToken);
            if (user == null)
            {
                throw new NotFoundException("Owner of laundry shop is not exist");
            }
            return shop.MapToLaundryShopDto(_mapper, user.FullName);
        }
    }
}
