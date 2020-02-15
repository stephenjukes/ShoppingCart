using ShoppingCart.Subscriptions.NotificationTypes;
using ShoppingCart.Subscriptions.Users.UserNotifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Subscriptions.Users
{
    class Retailer : User
    {
        public Retailer(long userId, params IAddress[] contactDetails) : base(userId, contactDetails)
        {
        }

        //public override UserType Recipient { get; } = UserType.Retailer;
        public override UserNotificationBuilder UserNotificationBuilder { get; } = UserNotificationBuilders.Retailer;
    }
}
