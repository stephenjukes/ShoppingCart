using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShoppingCart.ShoppingBasketItems;
using ShoppingCart.ShoppingItem;
using ShoppingCart.Updated;

namespace ShoppingCart.ShoppingBasket
{
    // DO WE NEED A DESIGN PATTERN WHERE BASKET ITEMS ARE ALWAYS MATCHED TO SHOPPING CARTS?
    class DefaultBasket : IShoppingBasket
    {
        public IEnumerable<IShoppingBasketItem> Items { get; }

        public DefaultBasket()
        {
            // Instruction specify a paramaterless constructor
            // Name can be populated using the Item enum, Id can be populated by index. TaxRules is a problem.
        }

        public DefaultBasket(IEnumerable<IShoppingItem> items)
        {
            Items = items.Select(item => (IShoppingBasketItem)item);
        }




        // Add Functionality to limit according to stock
        public IShoppingBasketItem AddItem (IShoppingItem item) 
            => AddItem(item, 1);

        public IShoppingBasketItem AddItem(IShoppingItem item, int quantity) 
            => ModifyItems(item, quantity, (basketItem, added) => basketItem.Quantity += added);

        public IShoppingBasketItem RemoveItem(IShoppingBasketItem item)
            => ModifyItems(item, 0, (basketItem, added) => basketItem.Quantity = 0);

        private IShoppingBasketItem ModifyItems(IShoppingItem shoppingItem, int quantity, Action<IShoppingBasketItem, int> action)
        {
            foreach (var basketItem in Items)
            {
                if (basketItem.Id == shoppingItem.Id) action(basketItem, quantity);
            }

            return (IShoppingBasketItem)shoppingItem;
        }




        public decimal SubTotal => throw new NotImplementedException();

        public decimal Tax => throw new NotImplementedException();

        public decimal Total => throw new NotImplementedException();

        public event EventHandler<ShoppingUpdatedEventArgs> Updated;


    }
}
