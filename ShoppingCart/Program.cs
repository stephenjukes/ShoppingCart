using System;
using System.Linq;
using System.Collections.Generic;
using ShoppingCart.ShoppingBasket;
using ShoppingCart.ShoppingItem;
using ShoppingCart.TaxRules;
using ShoppingCart.Subscriptions.NotificationTypes;
using ShoppingCart.Subscriptions.Users;
using ShoppingCart.Updated;
using ShoppingCart.Subscriptions;
using System.IO;
using ShoppingCart.Common.Loggers;
using ShoppingCart.Subscriptions.NotificationSystems;

namespace ShoppingCart
{
    class Program
    {
        static void Main(string[] args)
        {          
            var shoppingBasket = new DefaultBasket();
            var consoleLogger = new ConsoleLogger();
            var communicationChannelBasePath = @"..\..\..\CommunicationChannels";

            var notificationSystems = new List<NotificationSystem>()
            {
                NotificationSystemFactory.CreateNotificationType<EmailNotificationSystem>(communicationChannelBasePath + @"\Emails", consoleLogger),
                NotificationSystemFactory.CreateNotificationType<TextNotificationSystem>(communicationChannelBasePath + @"\Phones", consoleLogger)
            };

            var users = new List<User>()
            {
                new Customer(1, new EmailAddress("john@yahoo.co.uk"), new PhoneNumber("12345678")),
                new Retailer(2,  new EmailAddress("tesco@org.uk"), new PhoneNumber("87654321"))
            };

            SubscribeNotificationSystemsToBasket(notificationSystems, shoppingBasket);
            SubscribeUsersToNotificationSystems(users, notificationSystems);
            RefreshDirectory(communicationChannelBasePath, "Emails", "Phones");

            var shoppingItem = new DefaultShoppingItem(1, Item.Apples, 1, null);

            consoleLogger.LogImportant("ITEM ADDED...");
            shoppingBasket.AddItem(shoppingItem);

            consoleLogger.LogImportant("ITEM REMOVED...");
            shoppingBasket.RemoveItem(shoppingItem);
        }

        private static void RefreshDirectory(string path, params string[] subDirectories)
        {
            var directory = new DirectoryInfo(path);

            if (directory.Exists) directory.Delete(true);
            directory.Create();

            foreach (var subDirectory in subDirectories)
            {
                directory.CreateSubdirectory(subDirectory);
            }
        }

        private static void SubscribeNotificationSystemsToBasket(IEnumerable<NotificationSystem> notificationSystems, IShoppingBasket shoppingBasket)
        {
            foreach (var notificationSystem in notificationSystems)
            {
                shoppingBasket.Updated += (object basket, ShoppingUpdatedEventArgs e) => notificationSystem.HandleUpdated(basket, e);
            }
        }

        private static void SubscribeUsersToNotificationSystems(IEnumerable<User> users, IEnumerable<NotificationSystem> notificationSystems)
        {
            foreach (var user in users)
            {
                foreach (var notificationSystem in notificationSystems)
                {
                    var contactDetail = user.ContactDetails.FirstOrDefault(c => c.GetType() == notificationSystem.CommunicationType);
                    user.Subscribe(notificationSystem, contactDetail);
                }
            }
        }
    }
}
