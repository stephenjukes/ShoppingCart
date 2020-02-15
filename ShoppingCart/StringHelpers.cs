using ShoppingCart.Updated;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart
{
    public static class StringHelpers
    {
        private static Dictionary<UpdateType, string> UpdateCompletionMapping { get; } = new Dictionary<UpdateType, string>()
        {
            { UpdateType.Add, "Added"},
            { UpdateType.Remove, "Removed" }
        };

        public static string Border(int size)
            =>  $"\n{new String('-', size)}\n";

        public static string UpdateCompleted(UpdateType update)
            => UpdateCompletionMapping[update];
    }
}
