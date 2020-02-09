using ShoppingCart.ShoppingBasket;
using ShoppingCart.Subscriptions.Users;
using ShoppingCart.Updated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ShoppingCart.Subscriptions.NotificationTypes
{
    public abstract class Notification
    {
        protected readonly List<Contact> _contacts = new List<Contact>();

        public virtual void HandleUpdated(object basket, ShoppingUpdatedEventArgs eventArgs)
        {
            foreach (var contact in _contacts)
            {
                var userNotification = contact.User.UserNotificationBuilder
                    .NotificationType(this)
                    .User(contact.User)
                    .EventArgs(eventArgs)
                    .Build();

                var message = FormatNotification(userNotification);
                SendNotification(message);
            }
        }

        public abstract Type CommunicationType { get; }

        protected abstract string FormatNotification(UserNotification userNotification);

        protected abstract void SendNotification(string message);

        public void Subscribe(Contact contact)
            => _contacts.Add(contact);

        public void Unsubscribe(Contact contact)
            => _contacts.Remove(contact);

        
    }
}
