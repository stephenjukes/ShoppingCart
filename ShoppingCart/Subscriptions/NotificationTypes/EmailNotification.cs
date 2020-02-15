using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ShoppingCart.ShoppingBasket;
using ShoppingCart.ShoppingBasketItems;
using ShoppingCart.Subscriptions.Users;
using ShoppingCart.Updated;

namespace ShoppingCart.Subscriptions.NotificationTypes
{
    public class EmailNotification : Notification
    { 
        public EmailNotification(string communicationChannel) : base(communicationChannel)
        {
        }

        public override Type CommunicationType { get; } = typeof(EmailAddress);
        protected override int MessageNumber { get; set; }

        protected override string FormatForNotificationSystem(UserNotification notification)
        {
            var lines = new string[] { notification.Title, notification.Message, notification.Totals, notification.Conclusion };
            return String.Join('\n', lines);
        }

        protected override void SendNotification(string address, string summary, string message)
        {
            var inbox = Path.Combine(_communicationChannel, address);
            var emailTitle = (++MessageNumber).ToString() + summary;

            Directory.CreateDirectory(inbox);
            File.AppendAllText(Path.Combine(inbox, emailTitle), message);
            Console.WriteLine(message + '\n');
        }

        public override string ToString() => "Email";
    }
}
