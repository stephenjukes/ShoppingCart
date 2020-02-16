using ShoppingCart.Common.Loggers;
using ShoppingCart.Subscriptions.NotificationTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Subscriptions.NotificationSystems
{
    // This is necessary if the basket item is to update subscribers upon instantiation
    public static class NotificationSystemFactory
    {
        public static List<NotificationSystem> NotificationSystems { get; } = new List<NotificationSystem>();

        public static TNotification CreateNotificationType<TNotification>(string basePath, ILogger logger) where TNotification : NotificationSystem
        {
            var notificationSystem = (TNotification)Activator.CreateInstance(typeof(TNotification), basePath, logger);
            NotificationSystems.Add(notificationSystem);

            return notificationSystem;
        }
    }
}
