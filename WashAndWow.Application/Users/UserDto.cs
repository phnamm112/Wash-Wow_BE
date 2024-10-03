using AutoMapper;
using Wash_Wow.Application.Common.Mappings;
using Wash_Wow.Domain.Entities;
using static Wash_Wow.Domain.Enums.Enums;

namespace Wash_Wow.Application.Users
{
    public class UserDto : IMapFrom<UserEntity>
    {
        public string ID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public Role Role { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UserEntity, UserDto>();
        }
        public UserDto()
        {

        }
        public UserDto(string id, string fullName, string email, string phoneNumber, Role role)
        {
            ID = id;
            FullName = fullName;
            Email = email;
            PhoneNumber = phoneNumber;
            Role = role;
        }
    }
}
