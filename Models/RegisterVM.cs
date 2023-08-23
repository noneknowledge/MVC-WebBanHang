using System.ComponentModel.DataAnnotations;

namespace MVC_template.Models
{
    public class RegisterVM
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
       
        public string UserName { get; set; } = null!;
        public string PassWord { get; set; } = null!;
        public string Address { get; set; }
        
        public string PhoneNumber { get; set; }

    }
}
