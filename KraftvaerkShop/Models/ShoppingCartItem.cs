using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KraftvaerkShop.Models
{
    public class ShoppingCartItem
    {
        public int ShoppingCartItemId { get; set; }
        [Required]
        public Product Product { get; set; }
        [Required]
        [MaxLength(20)]
        public int Amount { get; set; }
    }
}
