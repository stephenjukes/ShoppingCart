using ShoppingCart.TaxRules;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.ShoppingItem
{
    interface IShoppingItem
    {
        long Id { get; }
        Item Name { get; }
       //IEnumerable<ITaxRule> TaxRules { get; }
    }
}
