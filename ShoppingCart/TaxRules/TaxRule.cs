using ShoppingCart.ShoppingBasket;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.TaxRules
{
    public class TaxRule
    {
        protected void UpdateTotals(IShoppingBasket basket, IShoppingBasketItem item, decimal tax)
        {
            basket.Tax += tax;
            basket.Total += tax;
            item.Tax += tax;
            item.Total += tax;
        }
    }
}
