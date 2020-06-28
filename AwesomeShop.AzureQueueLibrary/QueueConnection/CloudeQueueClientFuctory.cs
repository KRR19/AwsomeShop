using AwesomeShop.AzureQueueLibrary.Infrastructure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeShop.AzureQueueLibrary.QueueConnection
{
    public interface ICloudeQueueClientFuctory
    {
        CloudQueueClient GetClient(); 
    }
    public class CloudeQueueClientFuctory : ICloudeQueueClientFuctory
    {
        private readonly QueueConfige _queueConfige;
        private CloudQueueClient _cloudQueueClient;

        public CloudeQueueClientFuctory(QueueConfige queueConfige)
        {
            _queueConfige = queueConfige;
        }
        public CloudQueueClient GetClient()
        {
            if(_cloudQueueClient != null)
            {
                return _cloudQueueClient;
            }
            var storageAccount = CloudStorageAccount.Parse(_queueConfige.QueueConnectionString);
            _cloudQueueClient = storageAccount.CreateCloudQueueClient();
            return _cloudQueueClient;
        }
    }
}
