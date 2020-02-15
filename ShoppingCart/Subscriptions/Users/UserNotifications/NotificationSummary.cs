using ShoppingCart.Subscriptions.NotificationTypes;
using ShoppingCart.Totals;
using ShoppingCart.Updated;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Subscriptions.Users.UserNotifications
{
    public class NotificationSummary
    {
        public NotificationSummary(User user, Notification notificationSystem, ITotals publisher, UpdateType updateType)
        {
            User = user;
            NotificationSystem = notificationSystem;
            Publisher = publisher;
            UpdateType = updateType;
        }

        public User User { get; }
        public Notification NotificationSystem { get; }
        public ITotals Publisher { get; }
        public UpdateType UpdateType { get; }

        public override string ToString()
            =>  $"{User.GetType().Name}{NotificationSystem}_{Publisher}Updated_Item{StringHelpers.UpdateCompleted(UpdateType)}";
    }
}
