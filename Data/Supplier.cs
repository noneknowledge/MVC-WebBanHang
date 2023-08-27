using System;
using System.Collections.Generic;

namespace MVC_template.Data
{
    public partial class Supplier
    {
        public Supplier()
        {
            Products = new HashSet<Product>();
        }

        public string SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
