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
            try{
            //var apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("glimpse.emailsender@gmail.com", "Glimpse");
            var subject = Mailsubject;
            var to = new EmailAddress(To, username);
            var plainTextContent = message;
            var htmlContent = $"Please confirm your account by clicking <a href='{callbackUrl}'>here</a>.";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            await client.SendEmailAsync(msg);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}