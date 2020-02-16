using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.TestModels
{
    public class TestNotificationSummary
    {
        public int UserId { get; set; }
        public string UserType { get; set; }
        public string CommunicationType { get; set; }
        public string Publisher { get; set; }
        public string UpdateType { get; set; }
    }
}
