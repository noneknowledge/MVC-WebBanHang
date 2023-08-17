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
        public string PaymentMethod { get; set; } = null!;
        public DateTime? OrderDate { get; set; }
        public int AmountPaid { get; set; }
        public string? CustomerId { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
