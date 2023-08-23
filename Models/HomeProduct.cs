namespace MVC_template.Models
{
    public class HomeProduct
    {
        public IEnumerable<MVC_template.Data.Product> newProducts { get; set; }
        public IEnumerable<MVC_template.Data.Product> topSaleProducts { get; set; }

    }
}
