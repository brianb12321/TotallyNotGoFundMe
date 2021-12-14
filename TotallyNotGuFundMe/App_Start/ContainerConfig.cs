using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using TotallyNotGuFundMe.Data;
using TotallyNotGuFundMe.Email;
using TotallyNotGuFundMe.Models;

namespace TotallyNotGuFundMe
{
    public static class ContainerConfig
    {
        public static IContainer BuildContainer()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<ApplicationDbContext>()
                .InstancePerRequest();

            builder.RegisterType<EventDataService>()
                .As<IEventDataService>()
                .InstancePerRequest();

            builder.RegisterType<TransactionDataService>()
                .As<ITransactionDataService>()
                .InstancePerRequest();

            builder.RegisterInstance(new SendGridEmailService(EmailServiceFactory.GetSendGridApiKey()))
                .As<IEmailService>()
                .SingleInstance();

            return builder.Build();
        }
    }
}