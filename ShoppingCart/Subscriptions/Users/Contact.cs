using ShoppingCart.Subscriptions.NotificationTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Subscriptions.Users
{
    public class Contact
    {
        public Contact(User user, IAddress address)
        {
            User = user;
            Address = address;
        }

        public User User { get; }
        public IAddress Address { get; }
    }
}
