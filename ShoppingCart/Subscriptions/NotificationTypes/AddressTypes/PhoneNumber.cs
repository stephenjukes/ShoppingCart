using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Subscriptions.NotificationTypes
{
    public class PhoneNumber : IAddress
    {
        public PhoneNumber(string phoneNumber)
        {
            Address = phoneNumber;
        }

        public string Address { get; }
    }
}
