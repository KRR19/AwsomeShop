using AwesomeShop.AzureQueueLibrary.Messages;
using Microsoft.Build.Utilities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeShopAzureFunction.Email
{
    public interface ISendEmailCommandHendler
    {
        Task<bool> Handle(SendEmailComand command);
    }
    public class SendEmailCommandHendler : ISendEmailCommandHendler
    {
        private readonly EmailConfige _emailConfige;

        public SendEmailCommandHendler(EmailConfige emailConfige)
        {
            _emailConfige = emailConfige;
        }
        public async Task<bool> Handle(SendEmailComand command)
        {
            using (var client = new SmtpClient(_emailConfige.Host, _emailConfige.Port)
            {
                Credentials = new NetworkCredential(_emailConfige.Sender, _emailConfige.Password),
                EnableSsl = true
            })
            using (var message = new MailMessage(_emailConfige.Sender, command.To, command.Subject, command.Body))
            {
                await client.SendMailAsync(message);
            }
            return true;
        }
    }
}
