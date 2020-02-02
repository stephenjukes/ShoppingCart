using ShoppingCart.TaxRules;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Updated
{
    public class ShoppingUpdatedEventArgs : EventArgs
    {
        public decimal SubTotal { get; }
        public IEnumerable<ITaxRule> TaxRules { get; }
    }
}
