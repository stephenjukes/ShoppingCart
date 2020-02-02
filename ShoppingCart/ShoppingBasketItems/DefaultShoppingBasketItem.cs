using System;
using System.Collections.Generic;
using System.Text;
using ShoppingCart.ShoppingItem;
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
            TaxRules = taxRules;
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
    }
}
