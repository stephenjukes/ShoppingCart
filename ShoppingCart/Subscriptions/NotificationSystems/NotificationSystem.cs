using ShoppingCart.Common.Loggers;
using ShoppingCart.ShoppingBasket;
using ShoppingCart.Subscriptions.Users;
using ShoppingCart.Updated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ShoppingCart.Subscriptions.NotificationSystems
{
    public abstract class NotificationSystem
    {
        protected readonly List<Contact> _contacts = new List<Contact>();
        protected readonly string _communicationChannel;
        protected readonly ILogger _logger;

        public NotificationSystem(string communicationChannel, ILogger logger)
        {
            _communicationChannel = communicationChannel;
            _logger = logger;
        }

        public abstract Type CommunicationType { get; }
        protected abstract int MessageNumber { get; set; }

        public void HandleUpdated(object basket, ShoppingUpdatedEventArgs eventArgs)
        {
            foreach (var contact in _contacts)
            {
                var userNotification = contact.User.UserNotificationBuilder
                    .NotificationType(this)
                    .User(contact.User)
                    .EventArgs(eventArgs)
                    .Build();

                BasketDatabase.NotificationSummaries.Add(userNotification.Summary);

                var message = FormatForNotificationSystem(userNotification);
                SendNotification(contact.Address.Code, userNotification.Title, message);
            }
        }

        protected abstract string FormatForNotificationSystem(UserNotification userNotification);

        protected abstract void SendNotification(string address, string summary, string message);

        public void Subscribe(Contact contact)
            => _contacts.Add(contact);

        public void Unsubscribe(Contact contact)
            => _contacts.Remove(contact);
    }
}
