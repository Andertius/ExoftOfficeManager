namespace ExoftOfficeManager.Application.Services
{
    public interface IEmailService
    {
        void SendEmailConfirmationEmail(string message, string to);
        void SendPasswordResetEmail(string message, string to);
    }
}
