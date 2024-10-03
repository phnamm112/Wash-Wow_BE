using AutoMapper;
using Wash_Wow.Domain.Entities;

namespace Wash_Wow.Application.Users
{
    public static class UserMappingExtension
    {
        public static UserDto MapToUserDto(this UserEntity projectFrom, IMapper mapper)
            => mapper.Map<UserDto>(projectFrom);
        public static List<UserDto> MapToUserDtoList(this IEnumerable<UserEntity> projectFrom, IMapper mapper)
            => projectFrom.Select(x => x.MapToUserDto(mapper)).ToList();
    }
}
