using ShoppingCart.ShoppingBasket;
using ShoppingCart.TaxRules;
using ShoppingCart.Totals;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Updated
{
    public class ShoppingUpdatedEventArgs : EventArgs
    {
        public ShoppingUpdatedEventArgs(ITotals totalsEntity, UpdateType update)
        {
            TotalsEntity = totalsEntity;
            Update = update;
        }

        public ITotals TotalsEntity { get; }
        public UpdateType Update { get; }
    }
}
