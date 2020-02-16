using ShoppingCart.Common.Loggers;
using ShoppingCart.ShoppingBasket;
using ShoppingCart.Subscriptions.NotificationTypes;
using ShoppingCart.Subscriptions.Users;
using ShoppingCart.Updated;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ShoppingCart.Subscriptions.NotificationSystems
{
    public class TextNotificationSystem : NotificationSystem
    {
        public TextNotificationSystem(string communicationChannel, ILogger logger) : base(communicationChannel, logger)
        {
        }

        public override Type CommunicationType { get; } = typeof(PhoneNumber);
        protected override int MessageNumber { get; set; }

        protected override string FormatForNotificationSystem(UserNotification notification)
        {
            var title = StringHelpers.Border(50) + notification.Title + StringHelpers.Border(50);
            var lines = new string[] {title, notification.Message, notification.Conclusion };

            return String.Join('\n', lines);
        }

        protected override void SendNotification(string address, string title, string message)
        {
            Directory.CreateDirectory(_communicationChannel);

            var phoneNumber = Path.Combine(_communicationChannel, address);
            File.AppendAllText(phoneNumber, message);

            _logger.LogInfo(message + '\n');
        }

        public override string ToString() => "Text";
    }
}
