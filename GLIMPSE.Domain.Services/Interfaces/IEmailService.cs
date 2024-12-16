using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using GLIMPSE.Domain.Models;

namespace GLIMPSE.Domain.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmail(string notificationType, List<User> usersToNotify);
        Task<MailMessage> SetupMessage(string username, List<User> usersToNotify, string notificationType);
    }
}