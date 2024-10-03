using AutoMapper;
using WashAndWow.Domain.Entities;

namespace WashAndWow.Application.Voucher
{
    public class VoucherProfile : Profile
    {
        public VoucherProfile()
        {
            CreateMap<VoucherEntity, VoucherDto>();
        }
    }

}
