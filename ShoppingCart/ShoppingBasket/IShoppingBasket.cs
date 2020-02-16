using System.Collections.Generic;
using ShoppingCart.ShoppingItem;
using ShoppingCart.Totals;
using ShoppingCart.Updated;

namespace ShoppingCart.ShoppingBasket
{
    public interface IShoppingBasket : ITotals, IUpdated
    {
        IShoppingBasketItem AddItem(IShoppingItem item);
        IShoppingBasketItem AddItem(IShoppingItem item, int quantity);
        IShoppingBasketItem RemoveItem(IShoppingItem item);
        IEnumerable<IShoppingBasketItem> Items { get; }
    }
}
