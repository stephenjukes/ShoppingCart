using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Subscriptions.NotificationTypes
{
    public class EmailAddress : IAddress
    {
        public EmailAddress(string emailAddress)
        {
            Address = emailAddress;
        }

        public string Address { get; }
    }
}
