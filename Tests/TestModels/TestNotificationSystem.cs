using ShoppingCart.Subscriptions.NotificationTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.TestModels
{
    public class TestNotificationSystem
    {
        public int Id { get; set; }
        public string NotificationSystemName { get; set; }
        public Notification ActualEntity { get; set; }
        public string CommunicationType { get; set; }
        public string CommunicationChannel { get; set; }
    }
}
