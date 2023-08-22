using System;
using System.Collections.Generic;

namespace MVC_template.Data
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public string OrderId { get; set; } = null!;
        public string? PaymentMethod { get; set; }
        public DateTime? OrderDate { get; set; }
        public int? AmountPaid { get; set; }
        public string CustomerId { get; set; } = null!;
        public string? Address { get; set; }
        public string? Phone { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
