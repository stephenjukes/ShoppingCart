using ShoppingCart.ShoppingBasket;
using ShoppingCart.Subscriptions.Users.UserNotifications;
using ShoppingCart.Totals;
using ShoppingCart.Updated;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Subscriptions.Users
{
    // Make an abstraction of this?
    public class UserNotification
    {
        public UserNotification(NotificationSummary summary, string title, string message, string totals, string conclusion)
        {
            Summary = summary;
            Title = title;
            Message = message;
            Totals = totals;
            Conclusion = conclusion;
        }

        public NotificationSummary Summary { get; }
        public string Title { get; }
        public string Message { get; }
        public string Totals { get; }
        public string Conclusion { get; }
    }
}
