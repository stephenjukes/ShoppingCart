using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShoppingCart.ShoppingBasket;
using ShoppingCart.ShoppingBasketItems;

namespace ShoppingCart.TaxRules
{
    public class ItemSubTotalPercentageTaxRule : TaxRule, ITaxRule
    {
        private decimal _percentage;

        public ItemSubTotalPercentageTaxRule(decimal percentage)
        {
            _percentage = percentage;
        }

        public decimal CalculateTax(IShoppingBasket basket, IShoppingBasketItem item)
        {
            var tax = _percentage * item.SubTotal;
            UpdateTotals(basket, item, tax);

            return tax;
        }


    }
}
