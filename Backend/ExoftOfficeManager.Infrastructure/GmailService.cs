using System.Net;
using System.Net.Mail;

using ExoftOfficeManager.Application.Services;
using ExoftOfficeManager.Infrastructure.Configuration;

namespace ExoftOfficeManager.Infrastructure
{
    public class GmailService : IEmailService
    {
        private readonly EmailConfig emailConfig;

        public void SendEmailConfirmationEmail(string message, string to)
        {
            using var mailMessage = new MailMessage(emailConfig.Address, to)
            {
                Subject = "Email Confirmation",
                Body = message,
                IsBodyHtml = true,
            };

            SendEmail(mailMessage);
        }

        public void SendPasswordResetEmail(string message, string to)
        {
            using var mailMessage = new MailMessage(emailConfig.Address, to)
            {
                Subject = "Password reset",
                Body = message,
                IsBodyHtml = true,
            };

            SendEmail(mailMessage);
        }

        private void SendEmail(MailMessage mailMessage)
        {
            var smtp = new SmtpClient(emailConfig.Host, emailConfig.Port)
            {
                Credentials = new NetworkCredential()
                {
                    UserName = emailConfig.Address,
                    Password = emailConfig.Password,
                },

                EnableSsl = true,
            };

            smtp.Send(mailMessage);
        }
    }
}
