using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using FluentAssertions;
using ShoppingCart;
using ShoppingCart.ShoppingBasket;
using ShoppingCart.ShoppingBasketItems;
using ShoppingCart.ShoppingItem;
using ShoppingCart.TaxRules;
using ShoppingCart.Totals;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Tests.TestModels;

namespace Tests
{
    [Binding]
    // Change this name while keeping steps bound
    // Comments needed for each assertion
    public class SpecFlowFeature1Steps
    {
        private IShoppingBasket _shoppingBasket;
        private IEnumerable<TestTaxRule> TaxRules = new TestTaxRule[0];
        private Exception _exception;

        [Given(@"a new basket is instantiated")]
        public void WhenANewBasketIsInstantiated()
        {
            _shoppingBasket = new DefaultBasket(Guid.NewGuid());
            //Probably not necessary now.
            //_shoppingItems = ((IEnumerable<Item>)Enum.GetValues(typeof(Item)))
            //    .Select((item, index) => new DefaultShoppingItem(index, item));
        }

        [Given(@"the following tax rules:")]
        public void GivenTheFollowingTaxRules(Table table)
        {
            //var b = a.TaxRule;
            //var c = typeof(TaxRules).GetField(b).GetValue(typeof(TaxRules));

            TaxRules = table
                .CreateSet<TestTaxRule>()
                .Select(row =>
                   {
                       try
                       {
                           return new TestTaxRule
                           {
                               Id = row.Id,
                               TaxRule = (ITaxRule)(typeof(TaxRules).GetField(row.RuleName).GetValue(typeof(TaxRules)))
                           };
                       }
                       catch(Exception e)
                       {
                           throw new ArgumentOutOfRangeException($"No tax rule found with name '{row.RuleName}' ");
                       }  
                   });
        }


        [Then(@"the basket has (\d+) items")]
        public void ThenTheBasketHasItems(int expectedNumberOfItems)
        {
            _shoppingBasket.Items.Should().HaveCount(expectedNumberOfItems);
        }

        [Then(@"all totals are (\d+)")]
        public void ThenAllTotalsAre(int expectedTotal)
        {
            var conflict = _shoppingBasket.Items.FirstOrDefault(i =>
                i.SubTotal != expectedTotal ||
                i.Tax != expectedTotal ||
                i.Total != expectedTotal
            );

            conflict.Should().BeNull($"At least one item does not have quantity of {expectedTotal}");
        }

        [Then(@"the item '(\w+)' has a quantity of (\d+)")]
        public void ThenTheItemHasAQuantityOf(string itemName, int expectedQuantity)
        {
            var basketItem = GetBasketItem(itemName);
            basketItem.Quantity.Should().Be(expectedQuantity);
        }

        [Then(@"A '([A-Za-z]*Exception)' exception is thrown")]
        public void ThenAExceptionIsThrown(string exceptionType)
        {
            // Check that this is method is acceptable
            var type = Type.GetType("System." + exceptionType);
            _exception.Should().BeOfType(type);
        }

        // Refactor into smaller methods???
        [When(@"the following items are added:")]
        public void WhenTheFollowingItemsAreAdded(Table table)
        {
            // Refactor to base steps???
            var shoppingItemsForBasket = table.CreateSet<TestShoppingItem>()
                .Select(row =>
                {
                    var taxRules = GetTaxRulesFromIds(row.TaxRuleIds);
                    return new TestShoppingItem
                    {
                        Quantity = row.Quantity,
                        ShoppingItem = new DefaultShoppingItem(row.Id, row.Name, row.UnitPrice.IntoDecimal(), taxRules)
                    };
                });

            foreach (var forBasket in shoppingItemsForBasket)
            {
                if (String.IsNullOrEmpty(forBasket.Quantity))
                {
                    _shoppingBasket.AddItem(forBasket.ShoppingItem);
                }
                else
                {
                    var isIntegerQuantity = int.TryParse(forBasket.Quantity, out int quantity);
                    if (!isIntegerQuantity) throw new ArgumentOutOfRangeException("Quanity is not a valid integer.");

                    try
                    {
                        _shoppingBasket.AddItem(forBasket.ShoppingItem, quantity);
                    }
                    catch (Exception e)
                    {
                        _exception = e;
                    }
                }
            }
        }



        [Then(@"the item '(\w+)' has a subtotal of '(.?[\d+,]+\.?\d{0,2}[a-z]?)'")]
        public void ThenTheItemHasASubtotalOfP(string itemName, string expectedSubtotal)
        {
            var basketItem = GetBasketItem(itemName);

            basketItem.SubTotal.Should().Be(expectedSubtotal.IntoDecimal());
        }

        [Then(@"the basket has a subtotal of '(.?[\d+,]+\.?\d{0,2}[a-z]?)'")]
        public void ThenTheBasketHasASubtotalOfP(string expectedSubtotal)
        {
            _shoppingBasket.SubTotal.Should().Be(expectedSubtotal.IntoDecimal());
        }

        [Then(@"the basket contains the following items:")]
        public void ThenTheBasketContainsTheFollowingItems(Table table)
        {
            // Ideally we should be creating an instance of DefaultShoppingBasketItem, but this causes problems.
            var expectedBasketItems = table.CreateSet<TestShoppingBasketItem>()
            .Select(row =>
                {
                    // what about validating other fields?
                    var isValidQuantity = int.TryParse(row.Quantity, out int quantity);
                    if (!isValidQuantity) throw new ArgumentOutOfRangeException("Quantity should be an integer value");

                    var taxRules = GetTaxRulesFromIds(row.TaxRuleIds);
                    return new DefaultShoppingBasketItem(row.Id, row.Name, row.UnitPrice, taxRules)
                        {
                            Quantity = quantity,
                            SubTotal = row.SubTotal.IntoDecimal(),
                            Tax = row.Tax.IntoDecimal(),
                            Total = row.Total.IntoDecimal()
                        };
                });

            _shoppingBasket.Items.Should().BeEquivalentTo(expectedBasketItems, options => options
                    .Excluding( o => o.UnitPrice )
                    .Excluding( o => o.TaxRules)
                );
        }

        [Then(@"the basket has the following totals:")]
        public void ThenTheBasketHasTheFollowingTotals(Table table)
        {
            var expectedBasketTotals = table.CreateSet<TestTotals>()
                .Select(row => new DefaultBasket(Guid.NewGuid())
                 {
                     SubTotal = row.SubTotal.IntoDecimal(),
                     Tax = row.Tax.IntoDecimal(),
                     Total = row.Total.IntoDecimal()
                })
                .FirstOrDefault();

            _shoppingBasket.Should().BeEquivalentTo(expectedBasketTotals, options => options.Excluding(o => o.Items));
        }



        private IShoppingBasketItem GetBasketItem(string item)
            =>_shoppingBasket.Items.FirstOrDefault(i => i.Name == item.ToEnum<Item>());

        private IEnumerable<ITaxRule> GetTaxRulesFromIds(IEnumerable<int> taxRuleIds)
        {
            if (taxRuleIds == null) return new ITaxRule[0];

            // throw exception if no TaxRule Id exists or TaxRule cannot be found by that name. (not necessarily here)
            return TaxRules
                .Where(t => taxRuleIds.Contains(t.Id))
                .Select(t => t.TaxRule);
        }
    }
}
