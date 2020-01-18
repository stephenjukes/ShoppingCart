using ShoppingCart.ShoppingItem;
using ShoppingCart.Totals;
using ShoppingCart.Updated;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.ShoppingBasket
{
    interface IShoppingBasket : ITotals, IUpdated
    {
        IShoppingBasketItem AddItem(IShoppingItem item);
        IShoppingBasketItem AddItem(IShoppingItem item, int quantity);
        IShoppingBasketItem RemoveItem(IShoppingBasketItem item);
 
        IEnumerable<IShoppingBasketItem> Items { get; }

        // ITotals
        // IUpdated
    }
}
