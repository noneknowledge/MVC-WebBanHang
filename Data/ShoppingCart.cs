using System;
using System.Collections.Generic;

namespace MVC_template.Data
{
    public partial class ShoppingCart
    {
        public string CustomerId { get; set; } = null!;
        public string ProductId { get; set; } = null!;
        public int? Quantity { get; set; }
        public int? Unit { get; set; }
        public int? Total { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
