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
    // Can probably be removed afterwards
    //public delegate void EventHandler<ShoppingUpdatedEventArgs>(IShoppingBasket shoppingBasket, ShoppingUpdatedEventArgs e);


    // DO WE NEED A DESIGN PATTERN WHERE BASKET ITEMS ARE ALWAYS MATCHED TO SHOPPING CARTS?
    public class DefaultBasket : IShoppingBasket
    {
        public DefaultBasket(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
        public IEnumerable<IShoppingBasketItem> Items { get; } = new List<IShoppingBasketItem>();
        // Instructions state that none of these should have setters!!!
        public decimal SubTotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
        public event EventHandler<ShoppingUpdatedEventArgs> Updated;

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

            basketItem.PublishUpdate(UpdateType.Add);
         
            // (calculate tax)
            foreach (var taxRule in basketItem.TaxRules)
            {
                taxRule.CalculateTax(this, basketItem);
            }

            items.Add(basketItem);

            // Is this repeating code in CalculateTax ???
            UpdateBasketTotals();

            // This needs to be changed so that items fire their own events (from their constructor?)
            Updated?.Invoke(
                this, 
                new ShoppingUpdatedEventArgs(this, UpdateType.Add)
            );

            return basketItem;
        }
            
        public IShoppingBasketItem RemoveItem(IShoppingItem item)
        {
            var items = (List<IShoppingBasketItem>)Items;
            var basketItem = items.FirstOrDefault(i => i.Name == item.Name);    // find by id when these are set properly

            if (basketItem == null) return null;
            
            items.Remove(basketItem);
            basketItem.PublishUpdate(UpdateType.Remove);
            //this.RemoveTax(basketItem.Tax);

            // Is this repeating code in CalculateTax ???
            UpdateBasketTotals();

            Updated?.Invoke(
                this,
                new ShoppingUpdatedEventArgs(this, UpdateType.Remove)
            );
            
            return basketItem;
        }

        public override string ToString() => "Basket";

        // Can this be done without repeating code? The only way I can think of is using reflection.
        private void UpdateBasketTotals()
        {
            SubTotal = Items.Sum(item => item.SubTotal);
            Tax = Items.Sum(item => item.Tax);
            Total = Items.Sum(item => item.Total);
        }
    }
}
