using AutoMapper;
using MediatR;
using Wash_Wow.Domain.Common.Exceptions;
using Wash_Wow.Domain.Repositories;

namespace Wash_Wow.Application.Users.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, UserLoginDto>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _repository;
        public LoginQueryHandler(IMapper mapper, IUserRepository usersRepository)
        {
            _repository = usersRepository;
            _mapper = mapper;
        }
        public async Task<UserLoginDto> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            var user = await _repository.FindAsync(x => x.Email == query.user.Email && x.DeletedAt == null, cancellationToken);
            if (user == null)
            {
                throw new NotFoundException($"Không tìm thấy tài khoản nào với email - {query.user.Email}");
            }
            if (user != null)
            {
                var checkPassword = _repository.VerifyPassword(query.user.Password, user.PasswordHash);
                if (checkPassword)
                {
                    return UserLoginDto.Create(user.Email, user.FullName, user.ID, user.Role.ToString());
                }
            }
            throw new NotFoundException("Tài khoản hoặc mật khẩu không đúng.");
        }
    }
}
