using ShoppingCart.ShoppingItem;
using System.Collections.Generic;

namespace Tests
{
    public class TestShoppingItem
    {
        public string Quantity { get; set; }    // distinguish between 0 and null
        public int Id { get; set; }
        public Item Name { get; set; }
        public string UnitPrice { get; set; }   // allows currency symbols in feature
        public IEnumerable<int> TaxRuleIds { get; set; }
        public IShoppingItem ShoppingItem { get; set; }
    }
}
