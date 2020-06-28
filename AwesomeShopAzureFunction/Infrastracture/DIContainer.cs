using AwesomeShop.AzureQueueLibrary.QueueConnection;
using AwesomeShopAzureFunction.Email;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AwesomeShopAzureFunction.Infrastracture
{
    public sealed class DIContainer
    {
        public static readonly IServiceProvider _instance = Build();
        public static  IServiceProvider Instance => _instance;

        static DIContainer()
        {

        }
        private DIContainer()
        {

        }

        private static IServiceProvider Build()
        {
            var services = new ServiceCollection();
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
            services.AddSingleton(new EmailConfige
                (configuration["EmailHost"],
                 Convert.ToInt32(configuration["EmailPort"]),
                 configuration["EmailSender"],
                 configuration["EmailPassword"]
                ));
            services.AddSingleton<ISendEmailCommandHendler, SendEmailCommandHendler>();
            services.AddAzureQueueLibrary(configuration["AzureWebJobsStorage"]);
            return services.BuildServiceProvider();
        }
    }
}
