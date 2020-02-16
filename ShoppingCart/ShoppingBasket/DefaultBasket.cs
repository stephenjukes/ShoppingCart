using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShoppingCart.ShoppingBasketItems;
using ShoppingCart.ShoppingItem;
using ShoppingCart.TaxRules;
using ShoppingCart.Totals;
using ShoppingCart.Updated;

namespace ShoppingCart.ShoppingBasket
{
    public class DefaultBasket : IShoppingBasket
    {
        public IEnumerable<IShoppingBasketItem> Items { get; } = new List<IShoppingBasketItem>();
        public decimal SubTotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
        public event EventHandler<ShoppingUpdatedEventArgs> Updated;

        public IShoppingBasketItem AddItem(IShoppingItem item) 
            => AddItem(item, 1);

        public IShoppingBasketItem AddItem(IShoppingItem item, int quantity)
        {
            var items = (List<IShoppingBasketItem>)Items;
            if (quantity < 1) throw new ArgumentOutOfRangeException("Item quantity cannot be less than 1");

            RemoveItem(item); // if exists.

            var basketItem = CreateBasketItem(item.Id, item.Name, item.UnitPrice, quantity, item.TaxRules);
            Total = SubTotal += basketItem.SubTotal;

            basketItem.PublishUpdate(UpdateType.Add);

            CalculateTax(this, basketItem);
            items.Add(basketItem);

            Updated?.Invoke(
                this, 
                new ShoppingUpdatedEventArgs(this, UpdateType.Add)
            );

            return basketItem;
        }

        private IShoppingBasketItem CreateBasketItem(long id, Item name, decimal unitPrice, int quantity, IEnumerable<ITaxRule> taxRules)
        {
            var subTotal = unitPrice * quantity;
            var basketItem = new DefaultShoppingBasketItem(id, name, unitPrice, taxRules)
            {
                Quantity = quantity,
                SubTotal = subTotal,
                Total = subTotal
            };

            return basketItem;
        }

        private void CalculateTax(IShoppingBasket basket, IShoppingBasketItem basketItem)
        {
            foreach (var taxRule in basketItem.TaxRules)
            {
                taxRule.CalculateTax(basket, basketItem);
            }
        }

        public IShoppingBasketItem RemoveItem(IShoppingItem item)
        {
            var items = (List<IShoppingBasketItem>)Items;
            var basketItem = items.FirstOrDefault(i => i.Id == item.Id);

            if (basketItem == null) return null;
            
            items.Remove(basketItem);
            basketItem.PublishUpdate(UpdateType.Remove);

            Updated?.Invoke(
                this,
                new ShoppingUpdatedEventArgs(this, UpdateType.Remove)
            );
            
            return basketItem;
        }

        public override string ToString() => "Basket";
    }
}
