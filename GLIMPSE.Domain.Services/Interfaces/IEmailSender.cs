namespace GLIMPSE.Domain.Services.Interfaces;

public interface IEmailSender
{
    public Task SendEmailAsync(string Mailsubject, string To, string username, string message, string callbackUrl);
}