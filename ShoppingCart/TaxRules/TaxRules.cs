using ShoppingCart.TaxRules.InstanceTaxRules;

namespace ShoppingCart.TaxRules
{
    public static class TaxRules
    {
        public static ITaxRule NoTax = new ItemSubTotalPercentageTaxRule(0m);
        public static ITaxRule Tax20Percent = new ItemSubTotalPercentageTaxRule(0.2m);
        public static ITaxRule Administration50pTax = new ItemFlateRateAdministrationTax(0.5m);
        public static ITaxRule Administration5poundTax = new ItemFlateRateAdministrationTax(5);

        public static ITaxRule BandedTax2020 = new ItemPriceBandedTax(
            new PriceTaxBand(0, 10, NoTax),
            new PriceTaxBand(10, 100, Administration5poundTax),
            new PriceTaxBand(100, null, Tax20Percent)
        );

    }
}
