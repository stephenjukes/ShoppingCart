using ShoppingCart.ShoppingBasket;
using ShoppingCart.Subscriptions.Users;
using ShoppingCart.Updated;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ShoppingCart.Subscriptions.NotificationTypes
{
    public class TextNotification : Notification
    {
        public TextNotification(string communicationChannel) : base(communicationChannel)
        {
        }

        public override Type CommunicationType { get; } = typeof(PhoneNumber);
        protected override int MessageNumber { get; set; }

        protected override string FormatForNotificationSystem(UserNotification notification)
        {
            var summary = StringHelpers.Border(70) + notification.Summary + StringHelpers.Border(70);
            var lines = new string[] {summary, notification.Message, notification.Conclusion };

            return String.Join('\n', lines);
        }

        protected override void SendNotification(string address, string summary, string message)
        {
            //Directory.CreateDirectory(_communicationChannel);

            //var phoneNumber = Path.Combine(_communicationChannel, address);
            //File.AppendAllText(phoneNumber, message);

            Console.WriteLine(message + '\n');
        }

        public override string ToString() => "Text";
    }
}
