﻿using KraftvaerkShop.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KraftvaerkShop.Models
{
    public class ShoppingCart
    {
        private readonly KraftvaerkShopContext _DBcontext;

        private ShoppingCart(KraftvaerkShopContext DBcontext)
        {
            _DBcontext = DBcontext;
        }

        public int ShoppingCartId { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            var context = services.GetService<KraftvaerkShopContext>();
            
            int cartId = 5;

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        public void AddToCart(Product product, int amount)
        {
            var shoppingCartItem = _DBcontext.ShoppingCartItems.SingleOrDefault(
                    s => s.Product.Id == product.Id && s.ShoppingCartId == ShoppingCartId);

            if(shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Product = product,
                    Amount = 1
                };

                _DBcontext.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }

            _DBcontext.SaveChanges();
        }

        public int RemoveFromCart(Product product)
        {
            var shoppingCartItem = _DBcontext.ShoppingCartItems.SingleOrDefault(
                    s => s.Product.Id == product.Id && s.ShoppingCartId == ShoppingCartId);

            var localAmount = 0;

            if(shoppingCartItem != null)
            {
                if(shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    _DBcontext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            _DBcontext.SaveChanges();

            return localAmount;
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ??
                    (ShoppingCartItems =
                        _DBcontext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                            .Include(s => s.Product)
                            .ToList());
        }

        public void ClearCart()
        {
            var cartItems = _DBcontext
                .ShoppingCartItems
                .Where(cart => cart.ShoppingCartId == ShoppingCartId);

            _DBcontext.ShoppingCartItems.RemoveRange(cartItems);

            _DBcontext.SaveChanges();
        }

        public double GetShoppingCartTotal()
        {
            var total = _DBcontext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Select(c => c.Product.Price * c.Amount).Sum();

            return total;
        }
    }
}
