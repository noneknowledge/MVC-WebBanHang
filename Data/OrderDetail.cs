using System;
using System.Collections.Generic;

namespace MVC_template.Data
{
    public partial class OrderDetail
    {
        public string OrderDetailId { get; set; } = null!;
        public string? OrderId { get; set; }
        public string? ProductId { get; set; }
        public int? Quantity { get; set; }
        public int? Unit { get; set; }
        public int? Total { get; set; }

        public virtual Order? Order { get; set; }
        public virtual Product? Product { get; set; }
    }
}
