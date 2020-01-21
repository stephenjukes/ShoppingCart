using System;
using System.Collections.Generic;
using System.Text;
using ShoppingCart.ShoppingItem;
using ShoppingCart.TaxRules;
using ShoppingCart.Updated;

namespace ShoppingCart.ShoppingBasketItems
{
    public class DefaultShoppingBasketItem : IShoppingBasketItem
    {
        public DefaultShoppingBasketItem(int index, Item item)
        {
        }

        // Is this really the only way I can populate readonly properties ???
        public DefaultShoppingBasketItem(long id, Item name, decimal subTotal, decimal tax, IEnumerable<ITaxRule> taxRules)
        {
            Id = id;
            Name = name;
            SubTotal = subTotal;
            Tax = tax;
            TaxRules = taxRules;
        }

        public int Quantity { get; set; }

        public long Id { get; }

        public Item Name { get; }

        public IEnumerable<ITaxRule> TaxRules { get; }

        public decimal SubTotal { get; }

        public decimal Tax { get; }

        public decimal Total { get; }

        public event EventHandler<ShoppingUpdatedEventArgs> Updated;
    }
}
