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
        public override Type CommunicationType { get; } = typeof(EmailAddress);

        protected override string FormatNotification(UserNotification notification)
        {
            var lines = new string[] { notification.Summary, notification.Message, notification.Totals, notification.Conclusion };
            return String.Join('\n', lines);
        }

        protected override void SendNotification(string message)
        {
            // Implement sending by email

            // For now, log to console
            Console.WriteLine(message + '\n');  
        }

        public override string ToString() => "email notification";
    }
}
