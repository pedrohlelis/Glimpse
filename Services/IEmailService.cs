using Glimpse.Models;

namespace Glimpse.Services;

public interface IEmailService
{
    public bool SendEmail(EmailDto request);
}