using KraftvaerkShop.Data;
using KraftvaerkShop.Interfaces;
using KraftvaerkShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KraftvaerkShop.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly KraftvaerkShopContext _DBcontext;

        public ProductRepository(KraftvaerkShopContext DBcontext)
        {
            _DBcontext = DBcontext;
        }

        public IEnumerable<Product> Products => _DBcontext.Product;

        public Product GetProductById(int productId) => _DBcontext.Product.FirstOrDefault(p => p.Id == productId);
    }
}
