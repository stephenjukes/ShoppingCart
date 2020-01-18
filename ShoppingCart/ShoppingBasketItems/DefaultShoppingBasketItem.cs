using System;
using System.Collections.Generic;
using System.Text;
using ShoppingCart.ShoppingItem;
using ShoppingCart.TaxRules;
using ShoppingCart.Updated;

namespace ShoppingCart.ShoppingBasketItems
{
    class DefaultShoppingBasketItem : IShoppingBasketItem
    {
        public int Quantity { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public long Id { get; }

        public string Name { get; }

        Item IShoppingItem.Name => throw new NotImplementedException();

        //public IEnumerable<ITaxRule> TaxRules { get; }

        public decimal SubTotal => throw new NotImplementedException();

        public decimal Tax => throw new NotImplementedException();

        public decimal Total => throw new NotImplementedException();

        public object Current => throw new NotImplementedException();



        public event EventHandler<ShoppingUpdatedEventArgs> Updated;

        // Enumerator on IShoppingBasketItem
        public bool MoveNext()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
