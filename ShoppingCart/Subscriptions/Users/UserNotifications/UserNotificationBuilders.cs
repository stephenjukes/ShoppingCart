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
        private static Dictionary<UpdateType, string> UpdateMapping { get; } = new Dictionary<UpdateType, string>()
        {
            { UpdateType.Add, "added"},
            { UpdateType.Remove, "removed" }
        };

        public static UserNotificationBuilder Customer { get; } = new UserNotificationBuilder()
            .Summary((notification, user, eventArgs) => StandardSummary(notification, user, eventArgs))
            .Message(eventArgs => $"Your {eventArgs.TotalsEntity} has just been updated. (Item {UpdateMapping[eventArgs.Update]})")
            .Totals(eventArgs =>
                    $"The details of this {eventArgs.TotalsEntity} are as follows:\n" +
                    $"Subtotal: {eventArgs.TotalsEntity.SubTotal}\nTax: {eventArgs.TotalsEntity.Tax}\nTotal: {eventArgs.TotalsEntity.Total}")
            .Conclusion("If you are unaware of this update, please contact us on the above number.");


        public static UserNotificationBuilder Retailer { get; } = new UserNotificationBuilder()
            .Summary((notification, user, EventArgs) => StandardSummary(notification, user, EventArgs))
            .Message(eventArgs => $"{eventArgs.TotalsEntity} updated. (Item {UpdateMapping[eventArgs.Update]})")
            .Totals(eventArgs =>
                    $"{eventArgs.TotalsEntity} details as follows:\n" +
                    $"Subtotal: {eventArgs.TotalsEntity.SubTotal}\nTax: {eventArgs.TotalsEntity.Tax}\nTotal: {eventArgs.TotalsEntity.Total}")
            .Conclusion("Log in for more details.");

        private static string StandardSummary(Notification notification, User user, ShoppingUpdatedEventArgs eventArgs)
        {
            var border = StringHelpers.Border(70);
            return border + $"{user.GetType().Name} {notification}\t{eventArgs.TotalsEntity} updated - Item {UpdateMapping[eventArgs.Update]}" + border;
        }
    };
}
