using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Wash_Wow.Domain.Enums.Enums;

namespace WashAndWow.Application.LaundryShops
{
    public class LaundryShopDto
    {
        public string Id { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public string PhoneContact { get; set; }
        public int TotalMachines { get; set; }
        public decimal Wallet { get; set; }
        public LaundryShopStatus Status { get; set; }
        public TimeSpan OpeningHour { get; set; }
        public TimeSpan ClosingHour { get; set; }
        public string OwnerID { get; set; }
    }

}
