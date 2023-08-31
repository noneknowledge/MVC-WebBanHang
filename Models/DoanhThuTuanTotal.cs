namespace MVC_template.Models
{
    public class DoanhThuTuanTotal
    {
        public int WeekNumber { get; set; }
        public DateTime WeekStart { get; set; }
        public DateTime WeekEnd { get; set; }
        public int TotalAmountPaid { get; set; }
    }
}
