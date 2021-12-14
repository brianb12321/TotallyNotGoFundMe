using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotallyNotGuFundMe.Email
{
    public interface IEmailService
    {
        Task SendEmailAsync(string destination, string subject, string bodyPlain, string bodyHtml);
    }
}