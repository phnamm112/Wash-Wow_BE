using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wash_Wow.Domain.Entities;
using Wash_Wow.Domain.Repositories;
using WashAndWow.Domain.Repositories;

namespace WashAndWow.Application.LaundryShops.Read
{
    public class GetLaundryShopByIdQueryHandler : IRequestHandler<GetLaundryShopByIdQuery, LaundryShopDto>
    {
        private readonly ILaundryShopRepository _repository;
        private readonly IMapper _mapper;

        public GetLaundryShopByIdQueryHandler(
            ILaundryShopRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<LaundryShopDto?> Handle(GetLaundryShopByIdQuery request, CancellationToken cancellationToken)
        {
            var shop = await _repository.FindAsync(x => x.ID == request.Id, cancellationToken);

            if (shop == null)
                return null;

            return _mapper.Map<LaundryShopDto>(shop);
        }
    }

}
