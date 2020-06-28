using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeShop.AzureQueueLibrary.Infrastructure
{
    public class QueueConfige
    {
        public string QueueConnectionString { get; set; }
        public QueueConfige()
        {

        }
        public QueueConfige(string queueConnectionString)
        {
            QueueConnectionString = queueConnectionString;
        }
    }
}
