using ShoppingCart.Subscriptions.Users.UserNotifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart
{
    public static class BasketDatabase
    {
        public static List<NotificationSummary> NotificationSummaries { get; } = new List<NotificationSummary>();
    }
}
