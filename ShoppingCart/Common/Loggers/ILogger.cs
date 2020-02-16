using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Common.Loggers
{
    public interface ILogger
    {
        void LogInfo(string message);
        void LogImportant(string message);
    }
}
