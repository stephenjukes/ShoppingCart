using ShoppingCart.ShoppingBasket;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.TaxRules
{
    public class TaxRule
    {
        // I probably would have avoided this side effect, but since basket did not have a TaxRules property of its own, I couldn't think of another reason for why the basket was used as a parameter in CalculateTax()
        protected void UpdateTotals(IShoppingBasket basket, IShoppingBasketItem item, decimal tax)
        {
            basket.Tax += tax;
            basket.Total += tax;
            item.Tax += tax;
            item.Total += tax;
        }
    }
}
