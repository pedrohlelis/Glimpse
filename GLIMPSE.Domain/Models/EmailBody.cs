using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;

namespace GLIMPSE.Domain.Models;

public class EmailBody
{
    public static MailMessage CardCreated()
    {
        MailMessage message = new MailMessage();
        message.Subject = "Card criado com sucesso";
        message.Body = "<h1>Card Created</h1>";
        
        return message;
    }
}