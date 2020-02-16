using ShoppingCart.TaxRules;

namespace Tests.TestModels
{
    class TestTaxRule
    {
        public int Id { get; set; }
        public string RuleName { get; set; }
        public ITaxRule ActualEntity { get; set; }
    }
}
