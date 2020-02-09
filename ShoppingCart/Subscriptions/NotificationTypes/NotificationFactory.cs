using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Subscriptions.NotificationTypes
{
    // This is necessary if the basket item is to update subscribers upon instantiation
    public static class NotificationFactory
    {
        public static List<Notification> NotificationTypes { get; } = new List<Notification>();

        public static TNotification CreateNotificationType<TNotification>() where TNotification : Notification, new()
        {
            var notificationType = (TNotification)Activator.CreateInstance(typeof(TNotification));
            NotificationTypes.Add(notificationType);

            return notificationType;
        }
    }
}
