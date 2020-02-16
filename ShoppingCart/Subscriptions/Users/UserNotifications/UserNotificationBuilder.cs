using ShoppingCart.Subscriptions.NotificationSystems;
using ShoppingCart.Subscriptions.NotificationTypes;
using ShoppingCart.Totals;
using ShoppingCart.Updated;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Subscriptions.Users.UserNotifications
{
    public class UserNotificationBuilder
    {
        private Func<NotificationSystem, User, ShoppingUpdatedEventArgs, NotificationSummary> _summaryFunc;
        private Func<NotificationSummary, string> _titleFunc;
        private Func<ShoppingUpdatedEventArgs, string> _messageFunc;
        private Func<ShoppingUpdatedEventArgs, string> _totalsFunc;
        private string _conclusionFunc;
        private NotificationSystem _notificationType;
        private User _user;
        private ShoppingUpdatedEventArgs _eventArgs;

        public UserNotificationBuilder Summary(Func<NotificationSystem, User, ShoppingUpdatedEventArgs, NotificationSummary> summaryFunc)
        {
            _summaryFunc = summaryFunc;
            return this;
        }

        public UserNotificationBuilder Title(Func<NotificationSummary, string> titleFunc)
        {
            _titleFunc = titleFunc;
            return this;
        }

        public UserNotificationBuilder Message(Func<ShoppingUpdatedEventArgs, string> messageFunc)
        {
            _messageFunc = messageFunc;
            return this;
        }

        public UserNotificationBuilder Totals(Func<ShoppingUpdatedEventArgs, string> totalsFunc)
        {
            _totalsFunc = totalsFunc;
            return this;
        }

        public UserNotificationBuilder Conclusion(string conclusionFunc)
        {
            _conclusionFunc = conclusionFunc;
            return this;
        }

        public UserNotificationBuilder NotificationType(NotificationSystem notificationType)
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
            var summary = _summaryFunc(_notificationType, _user, _eventArgs);

            return new UserNotification(
                summary,
                _titleFunc(summary),
                _messageFunc(_eventArgs),
                _totalsFunc(_eventArgs),
                _conclusionFunc
            );
        }
    }
}
