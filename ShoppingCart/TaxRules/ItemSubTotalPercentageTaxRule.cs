using System;
using System.Collections.Generic;
using System.Text;
using ShoppingCart.ShoppingBasket;

namespace ShoppingCart.TaxRules
{
    public class ItemSubTotalPercentageTaxRule : ITaxRule
    {
        public decimal CalculateTax(IShoppingBasket basket, IShoppingBasketItem item)
        {
            throw new NotImplementedException();
        }
    }
}
