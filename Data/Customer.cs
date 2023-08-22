using System;
using System.Collections.Generic;

namespace MVC_template.Data
{
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
            ShoppingCarts = new HashSet<ShoppingCart>();
            UserLogins = new HashSet<UserLogin>();
        }

        public string CustomerId { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }
        public virtual ICollection<UserLogin> UserLogins { get; set; }
    }
}
