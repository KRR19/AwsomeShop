using AwesomeShop.AzureQueueLibrary.Infrastructure;
using AwesomeShop.AzureQueueLibrary.MessageSerializer;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using System.Text;

namespace AwesomeShop.AzureQueueLibrary.QueueConnection
{
    public static class DependencyInjectionRegistry
    {
        public static IServiceCollection AddAzureQueueLibrary(this IServiceCollection service, string queueConnectionString)
        {
            service.AddSingleton(new QueueConfige(queueConnectionString));
            service.AddSingleton<ICloudeQueueClientFuctory, CloudeQueueClientFuctory>();
            service.AddSingleton<IMessageSerializer, JsonSerializer>();
            service.AddTransient<IQueueCommunicator, QueueCommunicator>();
            return service;
        }
    }
}
