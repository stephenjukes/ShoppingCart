using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ShoppingCart.Common.Loggers;
using ShoppingCart.ShoppingBasket;
using ShoppingCart.ShoppingBasketItems;
using ShoppingCart.Subscriptions.NotificationTypes;
using ShoppingCart.Subscriptions.Users;
using ShoppingCart.Updated;

namespace ShoppingCart.Subscriptions.NotificationSystems
{
    public class EmailNotificationSystem : NotificationSystem
    { 
        public EmailNotificationSystem(string communicationChannel, ILogger logger) : base(communicationChannel, logger)
        {
        }

        public override Type CommunicationType { get; } = typeof(EmailAddress);
        protected override int MessageNumber { get; set; }

        protected override string FormatForNotificationSystem(UserNotification notification)
        {
            var title = StringHelpers.Border(70) + notification.Title + StringHelpers.Border(70);
            var lines = new string[] { title, notification.Message, notification.Totals, notification.Conclusion };
            return String.Join('\n', lines);
        }

        protected override void SendNotification(string address, string title, string message)
        {
            var inbox = Path.Combine(_communicationChannel, address);
            var emailTitle = (++MessageNumber).ToString() + title;

            Directory.CreateDirectory(inbox);
            File.AppendAllText(Path.Combine(inbox, emailTitle), message);
            _logger.LogInfo(message + '\n');
        }

        public override string ToString() => "Email";
    }
}
