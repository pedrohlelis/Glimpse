using Glimpse.Models;

namespace Glimpse.Services;

public interface IEmailSender
{
    // public bool SendEmail(EmailDto request);
    public Task SendEmailAsync(string Mailsubject, string To, string username, string message);
}