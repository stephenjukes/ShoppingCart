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
    public class DefaultBasket : IShoppingBasket
    {
        // Is there a way that we can use the List methods???
        // INSTRUCTIONS DO NOT ALLOW 'SET' !!!
        // Cant this List be used?
        public IEnumerable<IShoppingBasketItem> Items { get; } = new List<IShoppingBasketItem>();

        public DefaultBasket()
        {
            Items = ((IEnumerable<Item>)Enum.GetValues(typeof(Item)))
                .Select((itemName, index) => 
                    (IShoppingBasketItem) new DefaultShoppingBasketItem(index, itemName, 0, 0, null) { Quantity = 0 })
                .ToList();
        }

        public DefaultBasket(IEnumerable<IShoppingItem> items)
        {
            // This may not run since it attempts to cast derived from base
            Items = items.Select(item => (IShoppingBasketItem)item);
        }

        // Add Functionality to limit according to stock
        public IShoppingBasketItem AddItem(IShoppingItem item)
            => AddItem(item, 1);

        public IShoppingBasketItem AddItem(IShoppingItem item, int quantity)
        {
            if (quantity < 1) throw new ArgumentOutOfRangeException("Item quantity cannot be less than 1");

            var basketItem = Items.FirstOrDefault(i => i.Id == item.Id);
            basketItem.Quantity = quantity;

            return basketItem;
        }
            

        public IShoppingBasketItem RemoveItem(IShoppingItem item)
        {
            var basketItem = item as IShoppingBasketItem;
            //Items.Remove(basketItem);
            return basketItem;
        }

        




        public decimal SubTotal => throw new NotImplementedException();

        public decimal Tax => throw new NotImplementedException();

        public decimal Total => throw new NotImplementedException();

        public event EventHandler<ShoppingUpdatedEventArgs> Updated;


    }
}
