using Glimpse.Models;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace Glimpse.Services
{
    public class EmailService : IEmailService
    {
        public bool SendEmail(EmailDto request)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("alan.kling@ethereal.email"));
                email.To.Add(MailboxAddress.Parse(request.To));
                email.Subject = request.Subject;
                email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

                using (var smtp = new SmtpClient())
                {
                    smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
                    smtp.Authenticate("alan.kling@ethereal.email", "eQVNaycSa5n6qWXbdG");
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