using System;
using System.Collections.Generic;
using System.Text;
using ShoppingCart.ShoppingBasket;

namespace ShoppingCart.TaxRules.InstanceTaxRules
{
    public class ItemFlateRateAdministrationTax : TaxRule, ITaxRule
    {
        private decimal _unitItemTax;

        public ItemFlateRateAdministrationTax(decimal unitItemTax)
        {
            _unitItemTax = unitItemTax;
        }

        public decimal CalculateTax(IShoppingBasket basket, IShoppingBasketItem item)
        {
            var tax = _unitItemTax * item.Quantity;
            UpdateTotals(basket, item, tax);

            return _unitItemTax;
        }
    }
}
