using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wash_Wow.Domain.Entities
{
    [NotMapped]
    public class LoginEntity
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
