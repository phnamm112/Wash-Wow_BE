using AutoMapper;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using Wash_Wow.Infrastructure.Persistence;
using Wash_Wow.Infrastructure.Repositories;
using WashAndWow.Domain.Entities;
using WashAndWow.Domain.Repositories;

namespace WashAndWow.Infrastructure.Repositories
{
    public class EmailVerifyRepository : RepositoryBase<EmailVerification, EmailVerification, ApplicationDbContext>, IEmailVerifyRepository
    {
        private readonly MailSettings _mailSettings;
        private readonly ApplicationDbContext _context;

        public EmailVerifyRepository(ApplicationDbContext dbContext, IMapper mapper, IOptions<MailSettings> mailSettings) : base(dbContext, mapper)
        {
            _context = dbContext;
            _mailSettings = mailSettings.Value;
        }

        public async Task SendConfirmationEmailAsync(string email, string confirmationUrl)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail));
            message.To.Add(new MailboxAddress("", email));
            message.Subject = "Please confirm your email";

            // Create the email body
            message.Body = new TextPart("html")
            {
                Text = $"<p>Please confirm your email by clicking the link below:</p><a href='{confirmationUrl}'>Confirm Email</a>"
            };

            // Send the email using MailKit's SMTP client
            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;  // Bypass certificate validation (for development)

                await client.ConnectAsync(_mailSettings.Host, _mailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);

                // Authenticate with the Gmail SMTP server
                await client.AuthenticateAsync(_mailSettings.Mail, _mailSettings.Password);

                // Send the email
                await client.SendAsync(message);

                // Disconnect from the SMTP server
                await client.DisconnectAsync(true);
            }
        }

        public async Task SendTokenResetPassword(string email, string token)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail));
            message.To.Add(new MailboxAddress("", email));
            message.Subject = "Forgot password confirmation";

            // Create the email body
            message.Body = new TextPart("html")
            {
                Text = $"<p>Here is your token: {token}<p>"
            };

            // Send the email using MailKit's SMTP client
            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;  // Bypass certificate validation (for development)

                await client.ConnectAsync(_mailSettings.Host, _mailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);

                // Authenticate with the Gmail SMTP server
                await client.AuthenticateAsync(_mailSettings.Mail, _mailSettings.Password);

                // Send the email
                await client.SendAsync(message);

                // Disconnect from the SMTP server
                await client.DisconnectAsync(true);
            }
        }
    }
}
