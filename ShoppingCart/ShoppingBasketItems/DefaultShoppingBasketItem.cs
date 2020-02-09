using System;
using System.Collections.Generic;
using System.Text;
using ShoppingCart.ShoppingItem;
using ShoppingCart.Subscriptions.NotificationTypes;
using ShoppingCart.TaxRules;
using ShoppingCart.Updated;

namespace ShoppingCart.ShoppingBasketItems
{
    public class DefaultShoppingBasketItem : IShoppingBasketItem
    {
        public DefaultShoppingBasketItem()
        { 
        }

        // Is this really the only way I can populate readonly properties ???
        public DefaultShoppingBasketItem(long id, Item name, decimal unitPrice, IEnumerable<ITaxRule> taxRules)
        {
            Id = id;
            Name = name;
            UnitPrice = unitPrice;
            TaxRules = taxRules ?? new ITaxRule[0];
            ExposeToSubscribers();
        }

        public int Quantity { get; set; }
        public long Id { get; }
        public Item Name { get; }
        public decimal UnitPrice { get; }
        public IEnumerable<ITaxRule> TaxRules { get; }
        // Instructions state that none of these should have setters!!!
        public decimal SubTotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
        public event EventHandler<ShoppingUpdatedEventArgs> Updated;

        // This seems a bit strange to me, but the only way I can think of given the item itself 
        // implements IUpdated and must be responsible for updating subscribers
        private void ExposeToSubscribers()
        {
            foreach (var notificationType in NotificationFactory.NotificationTypes)
            {
                Updated += (object shoppingBasketItem, ShoppingUpdatedEventArgs eventArgs)
                    => notificationType.HandleUpdated(shoppingBasketItem, eventArgs);
            }
        }

        public void PublishUpdate(UpdateType update)
        {
            Updated?.Invoke(
                this,
                new ShoppingUpdatedEventArgs(this,update)
            );
        }

        public override string ToString() => "Item";
    }
}
