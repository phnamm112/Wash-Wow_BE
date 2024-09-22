using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WashAndWow.Domain.Entities
{
    [Table("EmailVerification")]
    public class EmailVerification
    {
        public string UserID { get; set; }
        public string Token { get; set; }
        public DateTime ExpireTime { get; set; }
    }
}
