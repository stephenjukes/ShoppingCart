﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using ShoppingCart;
using ShoppingCart.ShoppingBasket;
using ShoppingCart.ShoppingBasketItems;
using ShoppingCart.ShoppingItem;
using ShoppingCart.Subscriptions;
using ShoppingCart.Subscriptions.NotificationSystems;
using ShoppingCart.Subscriptions.NotificationTypes;
using ShoppingCart.Subscriptions.Users;
using ShoppingCart.Subscriptions.Users.UserNotifications;
using ShoppingCart.TaxRules;
using ShoppingCart.Totals;
using ShoppingCart.Updated;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Tests.TestModels;

namespace Tests
{
    [Binding]
    public class ShoppingBasketSteps
    {
        private IShoppingBasket _shoppingBasket;
        private IEnumerable<TestTaxRule> _testTaxRules = new TestTaxRule[0];
        private Exception _exception;
        private Func<object> _invocation;
        private IEnumerable<TestNotificationSystem> _testNotificationSystems;
        private IEnumerable<User> _users;

        public object RuntimeMethodInfo { get; private set; }

        [Given(@"the basket database is empty")]
        public void GivenTheBasketDatabaseIsEmpty()
        {
            BasketDatabase.NotificationSummaries.Clear();
        }

        [Given(@"there are no user subscribers")]
        public void GivenThereAreNoUserSubscribers()
        {
            foreach(var notificationSystem in NotificationSystemFactory.NotificationSystems)
            {
                var actualMailingList = ((List<Contact>)(typeof(NotificationSystem).GetField("_contacts", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(notificationSystem)));
                actualMailingList.Clear();
            }
        }

        [Given(@"a new basket is instantiated")]
        public void WhenANewBasketIsInstantiated()
        {
            // amend to take concretion as parameter
            _shoppingBasket = new DefaultBasket();
        }

        [Given(@"the following tax rules:")]
        public void GivenTheFollowingTaxRules(Table table)
        {
            _testTaxRules = table.CreateSet<TestTaxRule>()
                .Select(row =>
                   {
                       try
                       {
                           return new TestTaxRule
                           {
                               Id = row.Id,
                               ActualEntity = (ITaxRule)(typeof(TaxRules).GetField(row.RuleName).GetValue(typeof(TaxRules)))
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

        [Then(@"A '([A-Za-z]*Exception)' exception is thrown with message '(.*)'")]
        public void ThenAExceptionIsThrownWithMessage(string expectedExceptionName, string expectedMessage)
        {
            // Better to use https://fluentassertions.com/exceptions/
            var expectedExceptionType = Type.GetType("System." + expectedExceptionName);
            _exception.Should().BeOfType(expectedExceptionType);
            _exception.Message.Should().Contain(expectedMessage);

            Console.WriteLine();
        }

        [When(@"the following items are added:")]
        public void WhenTheFollowingItemsAreAdded(Table table)
        {
            var itemsForBasket = table.CreateSet<TestShoppingItem>()
                .Select(row =>
                {
                    var taxRules = GetTaxRulesFromIds(row.TaxRuleIds);
                    return new TestShoppingItem
                    {
                        Quantity = row.Quantity,
                        ShoppingItem = CreateItem<DefaultShoppingItem>(row.Id, row.Name, row.UnitPrice.IntoDecimal(), taxRules)
                    };
                });

            foreach (var item in itemsForBasket)
            {
                Func<object> addItem = () => _shoppingBasket.AddItem(item.ShoppingItem);

            if (String.IsNullOrEmpty(item.Quantity))
                {
                    _shoppingBasket.AddItem(item.ShoppingItem);
                }
                else
                {
                    var isIntegerQuantity = int.TryParse(item.Quantity, out int quantity);
                    if (!isIntegerQuantity) throw new ArgumentOutOfRangeException("Quanity is not a valid integer.");

                    try
                    {
                        _shoppingBasket.AddItem(item.ShoppingItem, quantity);
                    }
                    catch (Exception e)
                    {
                        _exception = e;
                        _invocation = addItem;
                    }
                }
            }
        }

        private TItem CreateItem<TItem>(long id) where TItem : IShoppingItem
            => CreateItem<TItem>(id, (Item)id, 0, null);

        private TItem CreateItem<TItem>(long id, Item name, decimal unitPrice, IEnumerable<ITaxRule> taxRules) where TItem : IShoppingItem
            => (TItem)Activator.CreateInstance(typeof(TItem), id, name, unitPrice, taxRules);

        [Then(@"the basket has a subtotal of '(.?[\d+,]+\.?\d{0,2}[a-z]?)'")]
        public void ThenTheBasketHasASubtotalOfP(string expectedSubtotal)
        {
            _shoppingBasket.SubTotal.Should().Be(expectedSubtotal.IntoDecimal());
        }

        [Then(@"the basket contains the following items:")]
        public void ThenTheBasketContainsTheFollowingItems(Table table)
        {
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
                    .Excluding(o => o.UnitPrice)
                    .Excluding(o => o.TaxRules)
                );
        }

        [Then(@"the basket has the following totals:")]
        public void ThenTheBasketHasTheFollowingTotals(Table table)
        {
            var expectedBasketTotals = table.CreateSet<TestTotals>()
                .Select(row => new DefaultBasket
                 {
                     SubTotal = row.SubTotal.IntoDecimal(),
                     Tax = row.Tax.IntoDecimal(),
                     Total = row.Total.IntoDecimal()
                })
                .FirstOrDefault();

            _shoppingBasket.Should().BeEquivalentTo(expectedBasketTotals, options => options.Excluding(o => o.Items));
        }

        [Given(@"the following notification systems")]
        public void GivenTheFollowingNotificationSystems(Table table)
        {
            _testNotificationSystems = table.CreateSet<TestNotificationSystem>()    
                .Select(row =>
                {
                    var loggerType = Type.GetType($"ShoppingCart.Common.Loggers.{row.Logger}, ShoppingCart");
                    var logger = Activator.CreateInstance(loggerType, new object[0]);
                    var genericType = Type.GetType($"ShoppingCart.Subscriptions.NotificationSystems.{row.NotificationSystemName}, ShoppingCart");
                    var notificationCreator = typeof(NotificationSystemFactory).GetMethod("CreateNotificationType");
                    var notificationSystem = (NotificationSystem)notificationCreator
                        .MakeGenericMethod(genericType)
                        .Invoke(typeof(NotificationSystemFactory), new object[] { row.CommunicationChannel, logger });

                    return new TestNotificationSystem
                    {
                        Id = row.Id,
                        NotificationSystemName = row.NotificationSystemName,
                        ActualEntity = notificationSystem,
                        CommunicationType = row.CommunicationType,
                        CommunicationChannel = row.CommunicationChannel
                    };
                }).ToArray();
        }

        [Given(@"the following users")]
        public void GivenTheFollowingUsers(Table table)
        {
            _users = table.CreateSet<TestUser>()
                .Select(row =>
                {
                    var userType = Type.GetType($"ShoppingCart.Subscriptions.Users.{row.UserType}, ShoppingCart");
                    return (User)Activator.CreateInstance(userType, new object[] { row.Id, new EmailAddress(row.Email), new PhoneNumber(row.PhoneNumber) });
                });
        }

        [Given(@"all notification systems subscribe to the basket")]
        public void GivenAllNotificationSystemsSubscribeToTheBasket()
        {
            foreach (var testNotificationSystem in _testNotificationSystems)
            {
                _shoppingBasket.Updated += (object basket, ShoppingUpdatedEventArgs e) => testNotificationSystem.ActualEntity.HandleUpdated(basket, e);
            }
        }

        [When(@"the users subscribe as follows for the communication types:")]
        public void WhenTheUsersSubscribeAsFollowsForTheCommunicationTypes(Table table)
        {
            var userSubscriptions = table.CreateSet<TestUserSubscriptions>();

            foreach (var subscription in userSubscriptions)
            {
                var user = GetUserFromId(subscription.UserId);
                var notificationSystems = GetNotificationSystemFromCommunicationType(subscription.CommunicationTypes);

                foreach (var notificationSystem in notificationSystems)
                {
                    var contactDetail = user.ContactDetails.FirstOrDefault(c => c.GetType() == notificationSystem.CommunicationType);
                    user.Subscribe(notificationSystem, contactDetail);
                }
            }
        }

        [When(@"items '(.*)' are removed")]
        public void WhenItemsAreRemoved(string itemStringIds)
        {
            var itemIds = itemStringIds.Split(',').Select(s => int.Parse(s.Trim()));

            var basketItems = itemIds
                .Select(id => _shoppingBasket.Items.FirstOrDefault(i => i.Id == id))
                .Where(i => i != null);

            // for removal of non existant item scenario
            if (basketItems.Count() == 0) basketItems = itemIds.Select(id => CreateItem<DefaultShoppingBasketItem>(id));

            foreach (var basketItem in basketItems)
            {
                _shoppingBasket.RemoveItem(basketItem);
            }
        }

        [Then(@"only the following notifications are received")]
        public void ThenOnlyTheFollowingNotificationsAreReceived(Table table)
        {
            var expectedNotifications = table.CreateSet<TestNotificationSummary>()
                .Select(row =>
                {
                    var user = _users.FirstOrDefault(u => u.Id == row.UserId);
                    var notificationSystem = _testNotificationSystems.FirstOrDefault(n => n.CommunicationType == row.CommunicationType).ActualEntity;
                    var updateType = row.UpdateType.ToEnum<UpdateType>();
                    var publisherType = Assembly.Load("ShoppingCart").GetTypes()
                        .Where(t =>
                            typeof(ITotals).IsAssignableFrom(t)
                            && t.BaseType is Object
                            && t.GetMethod("ToString").Invoke(Activator.CreateInstance(t), new object[0]).ToString().ToLowerInvariant() == row.Publisher.ToLowerInvariant())
                        .FirstOrDefault();
                    var publisher = (ITotals)Activator.CreateInstance(publisherType);

                    return new NotificationSummary(user, notificationSystem, publisher, updateType);
                });

            var summaries = BasketDatabase.NotificationSummaries;
            BasketDatabase.NotificationSummaries.Should().BeEquivalentTo(expectedNotifications);
        }

        [Then(@"no notifications are received\.")]
        public void ThenNoNotificationsAreReceived_()
        {
            BasketDatabase.NotificationSummaries.Should().BeEmpty();
        }



        private IShoppingBasketItem GetBasketItem(string item)
            =>_shoppingBasket.Items.FirstOrDefault(i => i.Name == item.ToEnum<Item>());

        private IEnumerable<ITaxRule> GetTaxRulesFromIds(IEnumerable<int> taxRuleIds)
        {
            if (taxRuleIds == null) return new ITaxRule[0];

            return _testTaxRules
                .Where(t => taxRuleIds.Contains(t.Id))
                .Select(t => t.ActualEntity);
        }

        private User GetUserFromId(int userId)
            => _users.FirstOrDefault(u => u.Id == userId);

        
        private IEnumerable<NotificationSystem> GetNotificationSystemFromCommunicationType(IEnumerable<string> communicationTypes)
        {
            if (communicationTypes == null) return new NotificationSystem[0];

            return _testNotificationSystems
                .Where(n => communicationTypes.Contains(n.CommunicationType))
                .Select(n => n.ActualEntity);
        }
    }
}
