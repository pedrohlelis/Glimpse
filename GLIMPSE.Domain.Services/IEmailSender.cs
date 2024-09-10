

namespace GLIMPSE.Domain.Services;

public interface IEmailSender
{
    public Task SendEmailAsync(string Mailsubject, string To, string username, string message, string callbackUrl);
}