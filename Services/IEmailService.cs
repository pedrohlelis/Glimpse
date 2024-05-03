using Glimpse.Models;

namespace Glimpse.Services;

public interface IEmailSender
{
    public bool SendEmail(EmailDto request);
}