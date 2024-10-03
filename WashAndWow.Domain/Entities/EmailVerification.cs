using System.ComponentModel.DataAnnotations.Schema;

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
