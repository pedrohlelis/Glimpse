using System.Net.Mail;
using System.Reflection;
using GLIMPSE.Domain.Models;
using GLIMPSE.Domain.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace GLIMPSE.Domain.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration configuration;

        public EmailService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task SendEmail(string notificationType, List<User> usersToNotify)
        {
            string username = configuration.GetValue<string>("EmailUser");
            string password = configuration.GetValue<string>("EmailToken");

            /* usersToNotify = CheckUsersNotificationOptions(notificationType, usersToNotify); */

            MailMessage message = await SetupMessage(username, usersToNotify, notificationType);

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

        public async Task<MailMessage> SetupMessage(string username, List<User> usersToNotify, string notificationType)
        {
            var emailBodyType = typeof(EmailBody);
            
            var method = emailBodyType.GetMethod(notificationType, BindingFlags.Public | BindingFlags.Static);

            var message = method.Invoke(null, null) as MailMessage;

            message.From = new MailAddress(username);
            foreach (var user in usersToNotify)
            {
                message.To.Add(user.Email);
            }
            message.IsBodyHtml = true;

            return message;
        }

        /* public List<User> CheckUsersNotificationOptions(string notificationType, List<User> usersToNotify)
        {
            foreach (var user in usersToNotify)
            {
                var propertyInfo = typeof(NotificationOptions).GetProperty(notificationType);
                if (!(bool)propertyInfo.GetValue(notificationOptions))
                {
                    usersToNotify.Remove(user);
                }
            }
            return usersToNotify;
        } */
    }
}