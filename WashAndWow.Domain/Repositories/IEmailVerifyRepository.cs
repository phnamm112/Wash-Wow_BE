using Wash_Wow.Domain.Repositories;
using WashAndWow.Domain.Entities;

namespace WashAndWow.Domain.Repositories
{
    public interface IEmailVerifyRepository : IEFRepository<EmailVerification, EmailVerification>
    {
        Task SendConfirmationEmailAsync(string email, string confirmationUrl);
        Task SendTokenResetPassword(string email, string token);
    }
}
