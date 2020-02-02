using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShoppingCart.ShoppingBasketItems;
using ShoppingCart.ShoppingItem;
using ShoppingCart.Totals;
using ShoppingCart.Updated;

namespace ShoppingCart.ShoppingBasket
{
    // DO WE NEED A DESIGN PATTERN WHERE BASKET ITEMS ARE ALWAYS MATCHED TO SHOPPING CARTS?
    public class DefaultBasket : IShoppingBasket
    {
        public IEnumerable<IShoppingBasketItem> Items { get; } = new List<IShoppingBasketItem>();

        // Add Functionality to limit according to stock
        public IShoppingBasketItem AddItem(IShoppingItem item)
            => AddItem(item, 1);

        public IShoppingBasketItem AddItem(IShoppingItem item, int quantity)
        {
            var items = (List<IShoppingBasketItem>)Items;
            if (quantity < 1) throw new ArgumentOutOfRangeException("Item quantity cannot be less than 1");

            RemoveItem(item);   // if exists.

            // Remove concrete instantiation from here
            var subTotal = item.UnitPrice * quantity;
            var basketItem = new DefaultShoppingBasketItem(item.Id, item.Name, item.UnitPrice, item.TaxRules)
                {
                    Quantity = quantity,
                    SubTotal = subTotal,
                    Total = subTotal
                };
         
            // (calculate tax)
            foreach (var taxRule in basketItem.TaxRules)
            {
                taxRule.CalculateTax(this, basketItem);
            }

            items.Add(basketItem);
            UpdateBasketTotals();

            return basketItem;
        }
            

        public IShoppingBasketItem RemoveItem(IShoppingItem item)
        {
            var items = (List<IShoppingBasketItem>)Items;
            var basketItem = items.FirstOrDefault(i => i.Name == item.Name);    // find by id when these are set properly

            if (basketItem != null) // required if tax is to be removed here
            {
                items.Remove(basketItem);
                this.RemoveTax(basketItem.Tax);
                UpdateBasketTotals();
            }

            return basketItem;
        }

        // Can this be done without repeating code? The only way I can think of is using reflection.
        private void UpdateBasketTotals()
        {
            SubTotal = Items.Sum(item => item.SubTotal);
            Tax = Items.Sum(item => item.Tax);
            Total = Items.Sum(item => item.Total);
        }

        // Instructions state that none of these should have setters!!!
        public decimal SubTotal { get; set; }

        public decimal Tax { get; set; }

        public decimal Total { get; set; }

        public event EventHandler<ShoppingUpdatedEventArgs> Updated;
    }
}
