using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart
{
    public static class StringHelpers
    {
        public static string Border(int size)
            =>  $"\n{new String('-', size)}\n";
    }
}
