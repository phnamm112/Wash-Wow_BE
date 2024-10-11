using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wash_Wow.Application.Common.Interfaces;
using static Wash_Wow.Domain.Enums.Enums;

namespace WashAndWow.Application.Payment.AfterPayment
{
    public class AfterPaymentCommand : ICommand, IRequest<string>
    {
        public int PaymentID { get; set; }
        public string OrderID {  get; set; }
        public PaymentStatus Status { get; set; }
        public AfterPaymentCommand()
        {
            
        }
    }
}
