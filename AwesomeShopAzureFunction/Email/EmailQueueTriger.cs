using System;
using System.Threading.Tasks;
using AwesomeShop.AzureQueueLibrary.Messages;
using AwesomeShop.AzureQueueLibrary.QueueConnection;
using AwesomeShopAzureFunction.Infrastracture;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace AwesomeShopAzureFunction.Email
{
    public static class EmailQueueTriger
    {
        [FunctionName("EmailQueueTriger")]
        public static async Task Run([QueueTrigger(RouteNames.EmailBox, Connection = "AzureWebJobsStorage")]
                                string message, 
                                ILogger log)
        {
            try
            {
                var quecommunicator = DIContainer.Instance.GetService<IQueueCommunicator>();
                var command = quecommunicator.Read<SendEmailComand>(message);

                var handler = DIContainer.Instance.GetService<ISendEmailCommandHendler>();
                await handler.Handle(command);
            }
            catch (Exception ex)
            {

                log.LogError("Somethink went wrong");
                throw;
            }
        }
    }
}
