using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Updated
{
    public interface IUpdated
    {
        event EventHandler<ShoppingUpdatedEventArgs> Updated;
    }
}
