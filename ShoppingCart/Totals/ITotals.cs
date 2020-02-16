using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Totals
{
    // I'm not sure how basket totals can be updated without setters
    public interface ITotals
    {
        decimal SubTotal { get; set; }
        decimal Tax { get; set; }
        decimal Total { get; set; }
    }
}
