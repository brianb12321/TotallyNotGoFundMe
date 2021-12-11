using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace TotallyNotGuFundMe.Email
{
    public static class EmailServiceFactory
    {
        public static IEmailService UseSendGridFromConfig()
        {
            string apiKey = WebConfigurationManager.AppSettings["SENDGRID_API_KEY"];
            return new SendGridEmailService(apiKey);
        }
    }
}