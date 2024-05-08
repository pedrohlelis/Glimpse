using Glimpse.Models;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using MailKit.Security;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Glimpse.Services
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string Mailsubject, string To, string username, string message, string callbackUrl)
        {

        }
    }
}