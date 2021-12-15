using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TotallyNotGuFundMe.Email
{
    public static class EmailServiceFactory
    {
        public static string GetSendGridApiKey()
        {
            string apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            if (apiKey == null)
                throw new Exception("The API Key is not found in the environment SENDGRID_API_KEY");
                
            return apiKey;
        }
    }
}