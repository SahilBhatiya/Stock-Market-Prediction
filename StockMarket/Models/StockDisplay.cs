using System.ComponentModel.DataAnnotations;

namespace StockMarket.Models
{
    public class StockDisplay
    {
        [Key]
        public long Id { get; set; }
        public DateTime date { get; set; }
        public decimal ClosePrice { get; set; }
        public decimal Volume { get; set; }
    }
}
