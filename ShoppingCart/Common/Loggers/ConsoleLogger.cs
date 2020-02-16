using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Common.Loggers
{
    public class ConsoleLogger : ILogger
    {
        public void LogInfo(string message)
            => Log(ConsoleColor.White, message);


        public void LogImportant(string message)
            => Log(ConsoleColor.Red, message);

        private void Log(ConsoleColor foregroundColor, string message)
        {
            Console.ForegroundColor = foregroundColor;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
