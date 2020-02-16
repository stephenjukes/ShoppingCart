using ShoppingCart.ShoppingItem;
using System.Collections.Generic;

namespace Tests
{
    class TestShoppingBasketItem
    {
        public int Id { get; set; }
        public Item Name { get; set; }
        public decimal UnitPrice { get; set; }
        public string Quantity { get; set; }
        public string SubTotal { get; set; }    // string allows currency symbols in feature
        public string Total { get; set; }       // defined as string to allow currency symbols in feature
        public string Tax { get; set; }        
        public IEnumerable<int> TaxRuleIds { get; set; }
    }
}

