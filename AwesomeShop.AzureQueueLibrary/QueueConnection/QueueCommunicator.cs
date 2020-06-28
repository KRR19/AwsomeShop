﻿using AwesomeShop.AzureQueueLibrary.Messages;
using AwesomeShop.AzureQueueLibrary.MessageSerializer;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeShop.AzureQueueLibrary.QueueConnection
{
    public interface IQueueCommunicator
    {
        T Read<T>(string message);
        Task SendAsync<T>(T obj) where T : BaseQueueMessage;
    }
    public class QueueCommunicator : IQueueCommunicator
    {
        private readonly IMessageSerializer _messageSerializer;
        private readonly ICloudeQueueClientFuctory _cloudeQueueClientFuctory;

        public QueueCommunicator(IMessageSerializer messageSerializer, ICloudeQueueClientFuctory cloudeQueueClientFuctory)
        {
            _messageSerializer = messageSerializer;
            _cloudeQueueClientFuctory = cloudeQueueClientFuctory;
        }
        public T Read<T>(string message)
        {
            return _messageSerializer.Deserialize<T>(message);
        }

        public async Task SendAsync<T>(T obj) where  T : BaseQueueMessage
        {
            var queueReference = _cloudeQueueClientFuctory.GetClient().GetQueueReference(obj.Route);
            await queueReference.CreateIfNotExistsAsync();
            var serializedMessage = _messageSerializer.Serialize(obj);
            var queueMessage = new CloudQueueMessage(serializedMessage);
            await queueReference.AddMessageAsync(queueMessage);
        }
    }
}