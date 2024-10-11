using MediatR;
using Net.payOS.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wash_Wow.Application.Common.Interfaces;

namespace WashAndWow.Application.PaymentPayOS.CreatePaymentUrl
{
    public class CreatePaymentUrlCommand : IRequest<CreatePaymentResult>, ICommand
    {
        public string BuyerID {  get; set; }
        public string BookingID { get; set; }
        public long PaymentID {  get; set; }
        public CreatePaymentUrlCommand()
        {

        }
    }
}
