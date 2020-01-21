using ShoppingCart.ShoppingItem;
using ShoppingCart.TaxRules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{
    class ShoppingBasketItem
    {
        public int Id { get; set; }
        public Item Name { get; set; }
        public string SubTotal { get; set; }
        public decimal Tax { get; set; }
        public IEnumerable<ITaxRule> TaxRules { get; set; }
    }
}
