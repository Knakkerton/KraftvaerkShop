using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KraftvaerkShop.Data;
using KraftvaerkShop.Interfaces;
using KraftvaerkShop.Models;
using KraftvaerkShop.Repositories;
using KraftvaerkShop.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace KraftvaerkShop.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ShoppingCart _shoppingCart;
        //private readonly IProductRepository _productRepository;
        static KraftvaerkShopContext _DBcontext;

        //some problem getting interface to work with shoppincart constructor. Needed to bring KraftvaerkShopContext here and use new repository
        //Inconsistent accessibility - error
        public ShoppingCartController(ShoppingCart shoppingCart, KraftvaerkShopContext DBcontext)
        {
            _shoppingCart = shoppingCart;
            _DBcontext = DBcontext;
        }

        ProductRepository _productRepository = new ProductRepository(_DBcontext);

        public ViewResult Index()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var sCVM = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };

            return View(sCVM);
        }

        public RedirectToActionResult AddToShoppingCart(int productId)
        {
            var selectedProduct = _productRepository.Products.FirstOrDefault(p => p.Id == productId);

            if (selectedProduct != null)
            {
                _shoppingCart.AddToCart(selectedProduct, 1);
            }
            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoveFromShoppingCart(int productId)
        {
            var selectedProduct = _productRepository.Products.FirstOrDefault(p => p.Id == productId);

            if (selectedProduct != null)
            {
                _shoppingCart.RemoveFromCart(selectedProduct);
            }
            return RedirectToAction("Index");
        }
    }
}