using ShoppingCart.TaxRules;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.ShoppingItem
{
    public class DefaultShoppingItem : IShoppingItem
    {
        public DefaultShoppingItem()
        { 
        }

        public DefaultShoppingItem(long id, Item name, decimal unitPrice, IEnumerable<ITaxRule> taxRules)
        {
            Id = id;
            Name = name;
            UnitPrice = unitPrice;
            TaxRules = taxRules;
        }

        public long Id { get; }
        public Item Name { get; }   // of type Item, since we're using enums
        public decimal UnitPrice { get; }
        public IEnumerable<ITaxRule> TaxRules { get; }
    }
}
