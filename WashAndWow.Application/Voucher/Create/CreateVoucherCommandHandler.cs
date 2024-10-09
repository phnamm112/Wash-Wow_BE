using AutoMapper;
using MediatR;
using Wash_Wow.Application.Common.Interfaces;
using Wash_Wow.Domain.Common.Exceptions;
using Wash_Wow.Domain.Repositories;
using WashAndWow.Domain.Entities;
using WashAndWow.Domain.Repositories;
using static Wash_Wow.Domain.Enums.Enums;

namespace WashAndWow.Application.Voucher.Create
{
    public class CreateVoucherCommandHandler : IRequestHandler<CreateVoucherCommand, string>
    {
        private readonly IVoucherRepository _voucherRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserRepository _userRepository;

        public CreateVoucherCommandHandler(IVoucherRepository voucherRepository
            , IMapper mapper
            , ICurrentUserService currentUserService
            , IUserRepository userRepository)
        {
            _voucherRepository = voucherRepository;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _userRepository = userRepository;
        }

        public async Task<string> Handle(CreateVoucherCommand request, CancellationToken cancellationToken)
        {
            var existVoucher = await _voucherRepository.AnyAsync(x => x.Name == request.Name && x.DeletedAt == null, cancellationToken);
            if (existVoucher)
            {
                throw new DuplicationException("Voucher name has exist");
            }
            var currentUser = await _userRepository.FindAsync(x => x.ID == _currentUserService.UserId && x.DeletedAt == null, cancellationToken);
            if (currentUser == null || (!currentUser.Role.Equals(Role.Admin) && !currentUser.Role.Equals(Role.ShopOwner)))
            {
                throw new UnauthorizedException("Method not allow ! Unauthorized");
            }
            VoucherEntity voucher = new VoucherEntity
            {
                Amount = request.Amount,
                ConditionOfUse = request.ConditionOfUse,
                ExpiryDate = request.ExpiryDate,
                MaximumReduce = request.MaximumReduce,
                MinimumReduce = request.MinimumReduce,
                ImgUrl = request.ImgUrl,
                Name = request.Name,
                Type = request.Type,
                CreatorID = _currentUserService.UserId,
                CreatedAt = DateTime.Now
            };
            _voucherRepository.Add(voucher);
            return await _voucherRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Success" : "Failed";
        }
    }
}
