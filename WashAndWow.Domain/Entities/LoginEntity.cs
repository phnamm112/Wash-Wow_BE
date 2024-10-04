using System.ComponentModel.DataAnnotations.Schema;

namespace Wash_Wow.Domain.Entities
{
    [NotMapped]
    public class LoginEntity
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
