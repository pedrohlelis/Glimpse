using GLIMPSE.Domain.Services.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace GLIMPSE.Domain.Services
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