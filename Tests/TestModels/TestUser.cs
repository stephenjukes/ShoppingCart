using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.TestModels
{
    public class TestUser
    {
        public long Id { get; set; }
        public string UserType { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
