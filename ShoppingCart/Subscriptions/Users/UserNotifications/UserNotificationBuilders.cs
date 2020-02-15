using ShoppingCart.ShoppingBasket;
using ShoppingCart.Subscriptions.NotificationTypes;
using ShoppingCart.Subscriptions.Users.UserNotifications;
using ShoppingCart.Totals;
using ShoppingCart.Updated;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Subscriptions.Users
{
    public static class UserNotificationBuilders
    {


        public static UserNotificationBuilder Customer { get; } = new UserNotificationBuilder()
            .Summary((notification, user, eventArgs) => GetNotificationSummary(notification, user, eventArgs))
            .Title(notificationSummary => notificationSummary.ToString())
            .Message(eventArgs => $"Your {eventArgs.TotalsEntity} has just been updated. (Item {StringHelpers.UpdateCompleted(eventArgs.Update)})")
            .Totals(eventArgs =>
                    $"The details of this {eventArgs.TotalsEntity} are as follows:\n" +
                    $"Subtotal: {eventArgs.TotalsEntity.SubTotal}\nTax: {eventArgs.TotalsEntity.Tax}\nTotal: {eventArgs.TotalsEntity.Total}")
            .Conclusion("If you are unaware of this update, please contact us on the above number.");


        public static UserNotificationBuilder Retailer { get; } = new UserNotificationBuilder()
            .Summary((notification, user, eventArgs) => GetNotificationSummary(notification, user, eventArgs))
            .Title(notificationSummary => notificationSummary.ToString())   // This could be something else
            .Message(eventArgs => $"{eventArgs.TotalsEntity} updated. (Item {StringHelpers.UpdateCompleted(eventArgs.Update)})")
            .Totals(eventArgs =>
                    $"{eventArgs.TotalsEntity} details as follows:\n" +
                    $"Subtotal: {eventArgs.TotalsEntity.SubTotal}\nTax: {eventArgs.TotalsEntity.Tax}\nTotal: {eventArgs.TotalsEntity.Total}")
            .Conclusion("Log in for more details.");

        private static NotificationSummary GetNotificationSummary(Notification notification, User user, ShoppingUpdatedEventArgs eventArgs)
            => new NotificationSummary(user, notification, eventArgs.TotalsEntity, eventArgs.Update);
    };
}
