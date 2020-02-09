using ShoppingCart.ShoppingBasket;
using ShoppingCart.Subscriptions.NotificationTypes;
using ShoppingCart.Subscriptions.Users;
using ShoppingCart.Subscriptions.Users.UserNotifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Subscriptions
{
    public abstract class User
    {
        private long _userId;

        public User(long userId, params IAddress[] contactDetails)
        {
            _userId = userId;
            ContactDetails = contactDetails;
        }

        public void Subscribe(Notification notificationType, IAddress address)
        {
            var contact = new Contact(this, address);
            notificationType.Subscribe(contact);
        }

        public void Unscubscribe(Notification notificationType, IAddress address)
        {
            var contact = new Contact(this, address);
            notificationType.Unsubscribe(contact);
        }

        public abstract UserType Recipient { get; }

        // It doesn't seem right that a user class should know about a user notification, but we'll do it like this for now.
        public abstract UserNotificationBuilder UserNotificationBuilder {get;}
        public IEnumerable<IAddress> ContactDetails { get; }

    }
}
