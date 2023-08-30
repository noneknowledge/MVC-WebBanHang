using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MVC_template.Models
{
    public class GlobalValues
    {
        public static string? CustomerID { get; set; }
        public static string? VaiTro { get; set; }
        public static string? Name { get; set; }

        public static void logOut()
        {
            CustomerID= null;
            VaiTro = null;
            Name= null;

        }
    }
}
