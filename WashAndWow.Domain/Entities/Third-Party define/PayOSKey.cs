using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WashAndWow.Domain.Entities.Third_Party_define
{
    [NotMapped]
    public class PayOSKey
    {
        public string ClientId { get; set; }
        public string ApiKey { get; set; }
        public string ChecksumKey { get; set; }
        public string Domain { get; set; }
    }
}
