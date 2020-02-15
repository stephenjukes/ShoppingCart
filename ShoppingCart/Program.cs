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

namespace ShoppingCart
{
    class Program
    {
        private static IShoppingBasket _shoppingBasket;
        private static List<Notification> _notificationSystems = new List<Notification>();
        private static List<User> _users = new List<User>();
        private static string _communicationChannelBasePath = @"..\..\..\CommunicationChannels";

        static void Main(string[] args)
        {
            _shoppingBasket = new DefaultBasket();

            _notificationSystems = new List<Notification>()
            {
                NotificationFactory.CreateNotificationType<EmailNotification>(_communicationChannelBasePath + @"\Emails"),
                NotificationFactory.CreateNotificationType<TextNotification>(_communicationChannelBasePath + @"\Phones")
            };

            _users = new List<User>()
            {
                new Customer(1, new EmailAddress("john@yahoo.co.uk"), new PhoneNumber("12345678")),
                new Retailer(2,  new EmailAddress("tesco@org.uk"), new PhoneNumber("87654321"))
            };

            SubscribeNotificationSystemsToBasket();
            SubscribeUsersToNotificationSystems();
            RefreshDirectory(_communicationChannelBasePath, "Emails", "Phones");

            var shoppingItem = new DefaultShoppingItem(1, Item.Apples, 1, null);

            var WriteInRed = WriteInColor(ConsoleColor.Red);

            WriteInRed("ITEM ADDED...");
            _shoppingBasket.AddItem(shoppingItem);

            WriteInRed("ITEM REMOVED...");
            _shoppingBasket.RemoveItem(shoppingItem);
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

        private static void SubscribeNotificationSystemsToBasket()
        {
            foreach (var notificationSystem in _notificationSystems)
            {
                _shoppingBasket.Updated += (object basket, ShoppingUpdatedEventArgs e) => notificationSystem.HandleUpdated(basket, e);
            }
        }

        private static void SubscribeUsersToNotificationSystems()
        {
            foreach (var user in _users)
            {
                foreach (var notificationSystem in _notificationSystems)
                {
                    var contactDetail = user.ContactDetails.FirstOrDefault(c => c.GetType() == notificationSystem.CommunicationType);
                    // The Subscribe method should be on the notification system only !!
                    user.Subscribe(notificationSystem, contactDetail);
                }
            }
        }

        private static Action<string> WriteInColor(ConsoleColor color)
        {
            return (string message) =>
            {
                Console.ForegroundColor = color;
                Console.WriteLine(message);
                Console.ResetColor();
            };
        }
    }
}
