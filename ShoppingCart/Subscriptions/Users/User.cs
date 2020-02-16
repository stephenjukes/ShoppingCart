using ShoppingCart.ShoppingBasket;
using ShoppingCart.Subscriptions.NotificationSystems;
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
        public long Id { get; }

        public User(long id, params IAddress[] contactDetails)
        {
            Id = id;
            ContactDetails = contactDetails;
        }

        public void Subscribe(NotificationSystem notificationType, IAddress address)
        {
            var contact = new Contact(this, address);
            notificationType.Subscribe(contact);
        }

        public void Unscubscribe(NotificationSystem notificationType, IAddress address)
        {
            var contact = new Contact(this, address);
            notificationType.Unsubscribe(contact);
        }

        // It doesn't seem right that a user class should know about a user notification, but for now...
        public abstract UserNotificationBuilder UserNotificationBuilder {get;}
        public IEnumerable<IAddress> ContactDetails { get; }

    }
}
