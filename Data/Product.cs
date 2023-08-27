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

        public int? Stt { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; } 
        public string? ProductDescription { get; set; }
        public int? Quantity { get; set; }
        public int? Prices { get; set; }
        public string? SupplierId { get; set; }
        public string? Image { get; set; }
        public string? IsHide { get; set; }

        public virtual Supplier? Supplier { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }
    }
}
