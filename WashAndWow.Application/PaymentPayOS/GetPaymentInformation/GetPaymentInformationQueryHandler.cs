using MailKit.Search;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Net.payOS;
using Net.payOS.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wash_Wow.Domain.Repositories;
using WashAndWow.Domain.Entities.Third_Party_define;
using WashAndWow.Domain.Repositories;

namespace WashAndWow.Application.PaymentPayOS.GetPaymentInformation
{
    public class GetPaymentInformationQueryHandler : IRequestHandler<GetPaymentInformationQuery, PaymentLinkInformation>
    {
        private readonly IConfiguration _config;
        private readonly PayOSKey _payOSKey;
        private readonly IBookingItemRepository _bookingRepository;
        private readonly IBookingItemRepository _bookingItemRepository;
        private readonly IUserRepository _userRepository;

        public GetPaymentInformationQueryHandler(IConfiguration config
            , IOptions<PayOSKey> payOSKey
            , IBookingItemRepository bookingRepository
            , IBookingItemRepository bookingItemRepository
            , IUserRepository userRepository)
        {
            _config = config;
            _payOSKey = payOSKey.Value;
            _bookingRepository = bookingRepository;
            _bookingItemRepository = bookingItemRepository;
            _userRepository = userRepository;
        }

        public async Task<PaymentLinkInformation> Handle(GetPaymentInformationQuery request, CancellationToken cancellationToken)
        {
            PayOS payOS = new PayOS(apiKey: _payOSKey.ApiKey, checksumKey: _payOSKey.ChecksumKey, clientId: _payOSKey.ClientId);
            
            PaymentLinkInformation paymentLinkInformation = await payOS.getPaymentLinkInformation((long)request.PaymentID);
            return paymentLinkInformation;
        }
    }
}
