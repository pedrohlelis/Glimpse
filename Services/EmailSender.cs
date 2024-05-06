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
        public async Task SendEmailAsync(string Mailsubject, string To, string username, string message)
        {
            //var apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
            var apiKey = "";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("glimpse.emailsender@gmail.com", "Glimpse");
            var subject = Mailsubject;
            var to = new EmailAddress(To, username);
            var plainTextContent = message;
            var htmlContent = "Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
        public bool SendEmail(EmailDto request)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("pedrohenri.lelis@gmail.com"));
                email.To.Add(MailboxAddress.Parse(request.To));
                email.Subject = request.Subject;
                email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

                using (var smtp = new SmtpClient())
                {
                    smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                    smtp.Authenticate("pedrohenri.lelis@gmail.com", "");
                    smtp.Send(email);
                    smtp.Disconnect(true);
                    return true;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

        }
    }
}