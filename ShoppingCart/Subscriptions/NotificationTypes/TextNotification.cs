using ShoppingCart.ShoppingBasket;
using ShoppingCart.Subscriptions.Users;
using ShoppingCart.Updated;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Subscriptions.NotificationTypes
{
    public class TextNotification : Notification
    {
        public override Type CommunicationType { get; } = typeof(PhoneNumber);

        protected override string FormatNotification(UserNotification notification)
        {
            var lines = new string[] { notification.Summary, notification.Message, notification.Conclusion };
            return String.Join('\n', lines);
        }

        protected override void SendNotification(string message)
        {
            // Implement sending by text

            // For now, log to console
            Console.WriteLine(message + '\n');
        }

        public override string ToString() => "text notification";
    }
}
