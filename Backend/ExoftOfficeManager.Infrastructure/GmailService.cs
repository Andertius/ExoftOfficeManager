using System.Net;
using System.Net.Mail;

using ExoftOfficeManager.Application.Services;
using ExoftOfficeManager.Infrastructure.Configuration;

using Microsoft.Extensions.Options;

namespace ExoftOfficeManager.Infrastructure
{
    public class GmailService : IEmailService
    {
        private readonly EmailConfig _emailConfig;

        public GmailService(IOptions<EmailConfig> opts)
        {
            _emailConfig = opts.Value;
        }

        public void SendEmailConfirmationEmail(string message, string to)
        {
            using var mailMessage = new MailMessage(_emailConfig.Address, to)
            {
                Subject = "Email Confirmation",
                Body = message,
                IsBodyHtml = true,
            };

            SendEmail(mailMessage);
        }

        public void SendPasswordResetEmail(string message, string to)
        {
            using var mailMessage = new MailMessage(_emailConfig.Address, to)
            {
                Subject = "Password reset",
                Body = message,
                IsBodyHtml = true,
            };

            SendEmail(mailMessage);
        }

        private void SendEmail(MailMessage mailMessage)
        {
            var smtp = new SmtpClient(_emailConfig.Host, _emailConfig.Port)
            {
                Credentials = new NetworkCredential()
                {
                    UserName = _emailConfig.Address,
                    Password = _emailConfig.Password,
                },

                EnableSsl = true,
            };

            smtp.Send(mailMessage);
        }
    }
}
