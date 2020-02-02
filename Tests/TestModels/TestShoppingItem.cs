using ShoppingCart.ShoppingItem;
using ShoppingCart.TaxRules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{
    public class TestShoppingItem
    {
        public string Quantity { get; set; }    // defined as string for steps to distinguish between int default value of 0
        public int Id { get; set; }
        public Item Name { get; set; }
        public string UnitPrice { get; set; }   // defined as string to allow currency symbols in feature
        public IEnumerable<int> TaxRuleIds { get; set; }
        public IShoppingItem ShoppingItem { get; set; }
    }
}
