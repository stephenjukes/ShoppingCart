using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.TaxRules.InstanceTaxRules
{
    public class PriceTaxBand
    {
        public decimal Threshold { get; }
        public decimal? Cap { get; }
        public ITaxRule TaxRule { get; }

        public PriceTaxBand(decimal threshold, decimal? cap, ITaxRule taxRule)
        {
            Threshold = threshold;
            Cap = cap;
            TaxRule = taxRule;
        }
    }
}
