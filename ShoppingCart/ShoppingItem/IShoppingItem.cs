using ShoppingCart.TaxRules;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.ShoppingItem
{
    public interface IShoppingItem
    {
        long Id { get; }
        Item Name { get; }
        decimal UnitPrice { get; }
        IEnumerable<ITaxRule> TaxRules { get; }
    }
}
