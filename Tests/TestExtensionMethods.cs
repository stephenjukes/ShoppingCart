using System;
using System.Text.RegularExpressions;

namespace Tests
{
    static class ExtensionMethods
    {
        public static decimal IntoDecimal(this string price)
        {
            if (String.IsNullOrEmpty(price)) return 0;

            var priceNumerics = new Regex(@"\d+\.?\d{0,2}").Match(price).Value;
            if (String.IsNullOrEmpty(priceNumerics))
                throw new FormatException("Unrecognised price format");

            if (price.Split('.').Length == 2)
                return decimal.Parse(priceNumerics);

            var hasUpperDenomination = Regex.IsMatch(price[0].ToString(), @"[^0-9]");
            return hasUpperDenomination ? decimal.Parse(priceNumerics) : decimal.Parse(priceNumerics) / 100;
        }

        public static EnumType ToEnum<EnumType>(this string itemName) where EnumType : struct, IConvertible
        {
            var isEnum = Enum.TryParse(itemName, true, out EnumType itemEnum);
            if (!isEnum) throw new InvalidOperationException($"Item name {itemName} is not recognised");

            return itemEnum;
        }
    }
}
