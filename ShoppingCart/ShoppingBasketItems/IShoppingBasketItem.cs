using ShoppingCart.ShoppingItem;
using ShoppingCart.Totals;
using ShoppingCart.Updated;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart
{
    public interface IShoppingBasketItem : IShoppingItem, ITotals, IUpdated
    {
        int Quantity { get; set; }
        void PublishUpdate(UpdateType update);
    }
}
