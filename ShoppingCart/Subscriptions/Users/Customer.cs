using ShoppingCart.ShoppingBasket;
using ShoppingCart.Subscriptions.NotificationTypes;
using ShoppingCart.Subscriptions.Users.UserNotifications;
using ShoppingCart.Updated;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Subscriptions.Users
{
    public class Customer : User
    {
        public Customer(long userId, params IAddress[] contactDetails) : base(userId, contactDetails)
        {
        }

        //public override UserType Recipient { get; } = UserType.Customer;
        public override UserNotificationBuilder UserNotificationBuilder { get; } = UserNotificationBuilders.Customer;
    }
}
