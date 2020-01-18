using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Updated
{
    interface IUpdated
    {
        event EventHandler<ShoppingUpdatedEventArgs> Updated;
    }
}
