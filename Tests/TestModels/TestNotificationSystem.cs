using ShoppingCart.Subscriptions.NotificationSystems;

namespace Tests.TestModels
{
    public class TestNotificationSystem
    {
        public int Id { get; set; }
        public string NotificationSystemName { get; set; }
        public NotificationSystem ActualEntity { get; set; }
        public string CommunicationType { get; set; }
        public string CommunicationChannel { get; set; }
        public string Logger { get; set; }
    }
}
