using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using FluentAssertions;
using ShoppingCart;
using ShoppingCart.ShoppingBasket;
using ShoppingCart.ShoppingBasketItems;
using ShoppingCart.ShoppingItem;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Tests
{
    [Binding]
    // Change this name while keeping steps bound
    public class SpecFlowFeature1Steps
    {
        private IShoppingBasket _shoppingBasket;
        //private IEnumerable<IShoppingItem> _shoppingItems;    // Probably not necessary now.
        private Exception _exception;

        [Given(@"a new basket is instantiated")]
        public void WhenANewBasketIsInstantiated()
        {
            _shoppingBasket = new DefaultBasket();
            //Probably not necessary now.
            //_shoppingItems = ((IEnumerable<Item>)Enum.GetValues(typeof(Item)))
            //    .Select((item, index) => new DefaultShoppingItem(index, item));
        }
        

        [Then(@"the basket has (\d+) items")]
        public void ThenTheBasketHasItems(int expectedNumberOfItems)
        {
            // Is there a better way message, given that conflict.Name will throw a NullReferenceException?
            var items = _shoppingBasket.Items.Where(i => i.Quantity > 0);
            items.Should().HaveCount(expectedNumberOfItems);
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

        [When(@"the item '(.*)' is added without an explicit quantity")]
        public void WhenTheItemIsAddedWithoutAnExplicitQuantity(string itemName)
        {
            var shoppingItem = (IShoppingItem)GetBasketItem(itemName);
            _shoppingBasket.AddItem(shoppingItem);
        }


        [Given(@"the item '(\w+)' is added with a quantity of (\d+)")]
        [When(@"the item '(\w+)' is added with a quantity of (-?\d+\.?\d*)")]
        public void WhenTheItemIsAddedWithAQuantityOf(string itemName, int quantity)
        {
            var shoppingItem = (IShoppingItem)GetBasketItem(itemName);
            try
            {
                _shoppingBasket.AddItem(shoppingItem, quantity);
            }
            catch (Exception e)
            {
                _exception = e;
            }
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

        [Given(@"a new basket with the following items:")]
        public void GivenANewBasketWithTheFollowingItems(Table table)
        {
            var basketItems = table.CreateSet<ShoppingBasketItem>()
                .Select(i => new DefaultShoppingBasketItem(i.Id, i.Name, i.SubTotal.IntoDecimal(), i.Tax, i.TaxRules));

            _shoppingBasket = new DefaultBasket(basketItems);
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

        private IShoppingBasketItem GetBasketItem(string itemName)
        {
            var isEnum = Enum.TryParse(itemName, true, out Item itemEnum);
            return _shoppingBasket.Items.FirstOrDefault(i => i.Name == itemEnum);
        }
    }
}
