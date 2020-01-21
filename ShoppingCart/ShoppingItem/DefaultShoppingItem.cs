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

        public DefaultShoppingItem(long id, Item name)
        {
            Id = id;
            Name = name;
        }

        public long Id { get; }
        public Item Name { get; }   // of type Item, since we're using enums
        public IEnumerable<ITaxRule> TaxRules { get; }
    }
}
