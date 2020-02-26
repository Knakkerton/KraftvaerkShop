using KraftvaerkShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KraftvaerkShop.Interfaces
{
    interface IProductRepository
    {
        IEnumerable<Product> Products { get; }

        Product GetProductById(int productId);
    }
}
