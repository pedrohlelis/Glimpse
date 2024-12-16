using System.Net.Mail;
using GLIMPSE.Domain.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Octokit;

namespace GLIMPSE.Domain.Services
{
    public class EmailService
    {
        private readonly IConfiguration configuration;

        public EmailService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task SendEmail(List<User> usersToNotify)
        {
            string username = configuration.GetValue<string>("EmailUser");
            string password = configuration.GetValue<string>("EmailToken");

            MailMessage message = await BuildMessage(username);

            using (SmtpClient smtp = new SmtpClient(configuration.GetValue<string>("EmailServer"), configuration.GetValue<int>("EmailPort")))
            {
                smtp.EnableSsl = true;
                smtp.Credentials = new System.Net.NetworkCredential(username, password);

                try
                {
                    smtp.Send(message);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public async Task<MailMessage> BuildMessage(string username, List<User> usersToNotify) 
        {
            string mailSubject = "Glimpse - Novo Card Adicionado no quadro";
            string mailBody = EmailBody.;

            MailMessage mensagem = new MailMessage();
            mensagem.From = new MailAddress(username);
            mensagem.To.Add(mailTo);
            mensagem.Subject = mailSubject;
            mensagem.IsBodyHtml = true;
            mensagem.Body = mailBody;

            return mensagem;
        }

        public async Task<List<User>> CheckUsersNotificationOptions(string username, List<User> usersToNotify) 
        {
            foreach (var user in usersToNotify)
            {
                var propertyInfo = typeof(NotificationOptions).GetProperty(attribute);
                if (propertyInfo != null && propertyInfo.PropertyType == typeof(bool))
                {
                    return (bool)propertyInfo.GetValue(notificationOptions);
                }
            }
        }
    }
}