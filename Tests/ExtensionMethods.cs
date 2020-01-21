using ShoppingCart;
using ShoppingCart.ShoppingItem;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Tests
{
    static class ExtensionMethods
    {
        public static decimal IntoDecimal(this string price)
        {

            var priceNumerics = new Regex(@"\d+\.?\d{0,2}").Match(price).Value;
            // Is this necessary given the step regex parameter constraint?
            if (String.IsNullOrEmpty(priceNumerics)) throw new ArgumentOutOfRangeException("Unrecognised price format");

            if (price.Split('.').Length == 2) return decimal.Parse(priceNumerics);

            var hasUpperDenomination = Regex.IsMatch(price[0].ToString(), @"[^0-9]");
            return hasUpperDenomination ? decimal.Parse(priceNumerics) : decimal.Parse(priceNumerics) / 100;
        }
    }
}
