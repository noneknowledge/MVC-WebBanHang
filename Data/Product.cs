using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;

namespace MVC_template.Data
{
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
            ShoppingCarts = new HashSet<ShoppingCart>();
        }

        public string? ProductId { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public string? ProductDescription { get; set; }
        public int? Quantity { get; set; }
        public int? Prices { get; set; }
        public string? SupplierId { get; set; }
        public string? Image { get; set; }

        [Required]
        public virtual Supplier? Supplier { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }
    }
}
