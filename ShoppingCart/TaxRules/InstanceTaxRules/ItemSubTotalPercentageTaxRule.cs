using ShoppingCart.ShoppingBasket;

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
