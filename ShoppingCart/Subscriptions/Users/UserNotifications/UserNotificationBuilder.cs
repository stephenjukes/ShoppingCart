using ShoppingCart.Subscriptions.NotificationTypes;
using ShoppingCart.Totals;
using ShoppingCart.Updated;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Subscriptions.Users.UserNotifications
{
    // I wonder if this could be abstracted
    // Consider allowing messages and totals to be part of a list
    public class UserNotificationBuilder
    {
        
        private Func<Notification, User, ShoppingUpdatedEventArgs, string> _summary;
        private Func<ShoppingUpdatedEventArgs, string> _message;
        private Func<ShoppingUpdatedEventArgs, string> _totals;
        private string _conclusion;
        private Notification _notificationType;
        private User _user;
        private ShoppingUpdatedEventArgs _eventArgs;

        public UserNotificationBuilder Summary(Func<Notification, User, ShoppingUpdatedEventArgs , string> summary)
        {
            _summary = summary;
            return this;
        }

        public UserNotificationBuilder Message(Func<ShoppingUpdatedEventArgs, string> message)
        {
            _message = message;
            return this;
        }

        public UserNotificationBuilder Totals(Func<ShoppingUpdatedEventArgs, string> totals)
        {
            _totals = totals;
            return this;
        }

        public UserNotificationBuilder Conclusion(string conclusion)
        {
            _conclusion = conclusion;
            return this;
        }

        public UserNotificationBuilder NotificationType(Notification notificationType)
        {
            _notificationType = notificationType;
            return this;
        }

        public UserNotificationBuilder User(User user)
        {
            _user = user;
            return this;
        }

        public UserNotificationBuilder EventArgs(ShoppingUpdatedEventArgs eventArgs)
        {
            _eventArgs = eventArgs;
            return this;
        }

        public UserNotification Build()
        {
            return new UserNotification(
                _summary(_notificationType, _user, _eventArgs),
                _message(_eventArgs),
                _totals(_eventArgs),
                _conclusion
            );
        }
    }
}
