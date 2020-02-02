using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Totals
{
    public interface ITotals
    {
        // Instructions state that none of these should have setters!!!
        decimal SubTotal { get; set; }
        decimal Tax { get; set; }
        decimal Total { get; set; }
    }
}
