using ShoppingCart.Subscriptions.NotificationTypes;
using ShoppingCart.Subscriptions.Users.UserNotifications;

namespace ShoppingCart.Subscriptions.Users
{
    public class Customer : User
    {
        public Customer(long userId, params IAddress[] contactDetails) : base(userId, contactDetails)
        {
        }

        public override UserNotificationBuilder UserNotificationBuilder { get; } = UserNotificationBuilders.Customer;
    }
}
