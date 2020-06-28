using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeShop.AzureQueueLibrary.Messages
{
    public class SendEmailComand : BaseQueueMessage
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public SendEmailComand() : base(RouteNames.EmailBox)
        {
        }
    }
}
