using System;
using System.Collections.Generic;
using System.Text;
using ShoppingCart.ShoppingBasket;
using ShoppingCart.ShoppingBasketItems;

namespace ShoppingCart.TaxRules.InstanceTaxRules
{
    public class ItemPriceBandedTax : TaxRule, ITaxRule
    {
        private IEnumerable<PriceTaxBand> _taxBands;

        public ItemPriceBandedTax(params PriceTaxBand[] taxBands)
        {
            _taxBands = taxBands;
        }

        public decimal CalculateTax(IShoppingBasket basket, IShoppingBasketItem item)
        {
            var unitItemTax = 0m;
            foreach(var band in _taxBands)
            {
                var applicableValue = (band.Cap ?? item.UnitPrice) - band.Threshold;
                // take this instantiation out of here
                var temporaryItem = new DefaultShoppingBasketItem { SubTotal = applicableValue };
                unitItemTax += band.TaxRule.CalculateTax(basket, temporaryItem);
            }

            var itemTax = unitItemTax * item.Quantity;
            UpdateTotals(basket, item, itemTax);

            return itemTax;
        }
    }
}
