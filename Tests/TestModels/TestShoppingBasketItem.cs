using ShoppingCart.ShoppingItem;
using ShoppingCart.TaxRules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{
    class TestShoppingBasketItem
    {
        public int Id { get; set; }
        public Item Name { get; set; }
        public decimal UnitPrice { get; set; }
        public string Quantity { get; set; }
        public string SubTotal { get; set; }    // defined as string to allow currency symbols in feature
        public string Total { get; set; }       // defined as string to allow currency symbols in feature
        public string Tax { get; set; }        // SHOULD THIS BE DEFIND AS AT STRING TOO ???
        public IEnumerable<int> TaxRuleIds { get; set; }
        //public IEnumerable<ITaxRule> TaxRules { get; set; }
    }
}

