﻿using ShoppingCart.ShoppingBasket;
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
        protected readonly string _communicationChannel;

        public Notification(string communicationChannel)
        {
            _communicationChannel = communicationChannel;
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
                SendNotification(contact.Address.Address, userNotification.Title, message);
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
