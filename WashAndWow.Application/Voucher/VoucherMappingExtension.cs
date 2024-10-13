using AutoMapper;
using WashAndWow.Domain.Entities;

namespace WashAndWow.Application.Voucher
{
    public static class VoucherMappingExtension
    {
        public static VoucherDto MapToVoucherDto(this VoucherEntity projectFrom, IMapper mapper)
            => mapper.Map<VoucherDto>(projectFrom);
        public static List<VoucherDto> MapToVoucherDtoList(this IEnumerable<VoucherEntity> projectFrom, IMapper mapper)
            => projectFrom.Select(x => x.MapToVoucherDto(mapper)).ToList();
    }
}
