using ShoppingCart.ShoppingBasket;
using ShoppingCart.Totals;
using ShoppingCart.Updated;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Subscriptions.Users
{
    // Make an abstraction of this?
    public class UserNotification
    {
        public UserNotification(string summary, string message, string totals, string conclusion)
        {
            Summary = summary;
            Message = message;
            Totals = totals;
            Conclusion = conclusion;
        }

        public string Summary { get; }
        public string Message { get; }
        public string Totals { get; }
        public string Conclusion { get; }
    }
}
