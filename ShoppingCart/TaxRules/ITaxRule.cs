using ShoppingCart.ShoppingBasket;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.TaxRules
{
    public interface ITaxRule
    {
        decimal CalculateTax(IShoppingBasket basket, IShoppingBasketItem item);
    }
}
