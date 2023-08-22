using System;
using System.Collections.Generic;

namespace MVC_template.Data
{
    public partial class UserLogin
    {
        public int Uid { get; set; }
        public string UserName { get; set; } = null!;
        public string PassWord { get; set; } = null!;
        public string? VaiTro { get; set; }
        public string? CustomerId { get; set; }

        public virtual Customer? Customer { get; set; }
    }
}
