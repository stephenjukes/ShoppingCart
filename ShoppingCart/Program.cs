using System;
using System.Linq;
using System.Collections.Generic;
using ShoppingCart.ShoppingBasket;
using ShoppingCart.ShoppingItem;
using ShoppingCart.TaxRules;

namespace ShoppingCart
{
    class Program
    {
        static void Main(string[] args)
        {
            var shoppingItems = ((IEnumerable<Item>)Enum.GetValues(typeof(Item)))
                .Select((item, index) => new DefaultShoppingItem(index, item));

            var shoppingBasket = new DefaultBasket(shoppingItems);
        }
    }
}
