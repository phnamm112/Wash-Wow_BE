using MediatR;
using Wash_Wow.Application.Common.Interfaces;
using static Wash_Wow.Domain.Enums.Enums;

namespace WashAndWow.Application.LaundryShops.Create
{
    public class CreateLaundryShopCommand : IRequest<string>, ICommand
    {
        public CreateLaundryShopCommand()
        {

        }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneContact { get; set; }
        public int TotalMachines { get; set; }
        public decimal Wallet { get; set; }
        public LaundryShopStatus Status { get; set; }
        public string OpeningHour { get; set; }
        public string ClosingHour { get; set; }
        public string OwnerID { get; set; }
    }
}
