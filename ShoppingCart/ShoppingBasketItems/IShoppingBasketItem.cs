using ShoppingCart.ShoppingItem;
using ShoppingCart.Totals;
using ShoppingCart.Updated;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart
{
    interface IShoppingBasketItem : IShoppingItem, ITotals, IUpdated, IEnumerator
    {
        int Quantity { get; set; }
    }
}
