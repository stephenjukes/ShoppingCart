using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.TestModels
{
    public class TestUserSubscriptions
    {
        public int UserId { get; set; }
        public IEnumerable<string> CommunicationTypes { get; set; }
    }
}
