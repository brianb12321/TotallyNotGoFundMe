using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace TotallyNotGuFundMe.Email
{
    public interface IEmailService
    {
        Task SendEmailAsync(string destination, string subject, string bodyPlain, string bodyHtml);
    }

    public class SendGridEmailService : IEmailService
    {
        private string apiKey;

        public SendGridEmailService(string apiKey)
        {
            this.apiKey = apiKey;
        }

        public async Task SendEmailAsync(string destination, string subject, string bodyPlain, string bodyHtml)
        {
            SendGridClient client = new SendGridClient(apiKey);
            var from = new EmailAddress("brian.jx.b@gmail.com", "Brian Barnes");
            var to = new EmailAddress(destination);
            SendGridMessage sgMessage =
                MailHelper.CreateSingleEmail(from, to, subject, bodyPlain, bodyHtml);
            await client.SendEmailAsync(sgMessage);
        }
    }
}