using ShoppingCart;
using ShoppingCart.ShoppingItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Tests.TestModels;

namespace Tests
{
    static class ExtensionMethods
    {
        public static decimal IntoDecimal(this string price)
        {
            if (String.IsNullOrEmpty(price))
                return 0;

            var priceNumerics = new Regex(@"\d+\.?\d{0,2}").Match(price).Value;
            if (String.IsNullOrEmpty(priceNumerics))
                throw new ArgumentOutOfRangeException("Unrecognised price format");    // Is this necessary given the step regex parameter constraint?

            if (price.Split('.').Length == 2)
                return decimal.Parse(priceNumerics);

            var hasUpperDenomination = Regex.IsMatch(price[0].ToString(), @"[^0-9]");
            return hasUpperDenomination ? decimal.Parse(priceNumerics) : decimal.Parse(priceNumerics) / 100;
        }

        public static T ToEnum<T>(this string itemName) where T : struct // Is struct correct?
        {
            var isEnum = Enum.TryParse(itemName, true, out T itemEnum);
            if (!isEnum) throw new InvalidOperationException($"Item name {itemName} is not recognised");

            return itemEnum;
        }

        public static  IEnumerable<TActual> GetEntitiesFromIds<TTest, TActual>(this IEnumerable<TTest> testEntities, IEnumerable<int> ids)
            where TTest : TestEntity<TActual>
        {
            if (ids == null) return new TActual[0];

            return testEntities
                .Where(t => ids.Contains(t.Id))
                .Select(t => (TActual)t.ActualEntity);
        }
    }
}
