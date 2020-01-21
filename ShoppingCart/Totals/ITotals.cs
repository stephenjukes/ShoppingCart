using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Totals
{
    public interface ITotals
    {
        decimal SubTotal { get; }
        decimal Tax { get; }
        decimal Total { get; }
    }
}
