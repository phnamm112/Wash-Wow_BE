using AutoMapper;
using Wash_Wow.Application.Common.Mappings;
using Wash_Wow.Domain.Entities;

namespace Wash_Wow.Application.Users
{
    public class UserLoginDto : IMapFrom<UserEntity>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UserEntity, UserLoginDto>();
        }
        public string Token { get; set; }
        public string Username { get; set; }
        public string Email {  get; set; }
        public string ID { get; set; }
        public string Role { get; set; }
        public static UserLoginDto Create(string email, string username, string id, string role)
        {
            return new UserLoginDto
            {
                Email = email,
                Username = username,
                ID = id,
                Role = role
            };
        }
    }
}
