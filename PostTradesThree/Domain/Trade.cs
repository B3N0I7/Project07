using System.ComponentModel.DataAnnotations;

namespace PostTradesThree.Domain
{
    public class Trade
    {
        public int TradeId { get; set; }

        [Required(ErrorMessage = "Account is required.")]
        [StringLength(50)]
        public string Account { get; set; }
        [Required(ErrorMessage = "Account Type is required.")]
        [StringLength(50)]
        public string AccountType { get; set; }
        [Range(0, Double.MaxValue)]
        public double? BuyQuantity { get; set; }
        [Range(0, Double.MaxValue)]
        public double? SellQuantity { get; set; }
        [Range(0, Double.MaxValue)]
        public double? BuyPrice { get; set; }
        [Range(0, Double.MaxValue)]
        public double? SellPrice { get; set; }
        public DateTime? TradeDate { get; set; }
        [Required(ErrorMessage = "Trade Security is required.")]
        [StringLength(50)]
        public string TradeSecurity { get; set; }
        [Required(ErrorMessage = "Trade Status is required.")]
        [StringLength(50)]
        public string TradeStatus { get; set; }
        [Required(ErrorMessage = "Trader is required.")]
        [StringLength(50)]
        public string Trader { get; set; }
        [Required(ErrorMessage = "Benchmark is required.")]
        [StringLength(50)]
        public string Benchmark { get; set; }
        [Required(ErrorMessage = "Book is required.")]
        [StringLength(50)]
        public string Book { get; set; }
        [Required(ErrorMessage = "Creation Name is required.")]
        [StringLength(50)]
        public string CreationName { get; set; }
        public DateTime? CreationDate { get; set; }
        [Required(ErrorMessage = "Revision Name is required.")]
        [StringLength(50)]
        public string RevisionName { get; set; }
        public DateTime? RevisionDate { get; set; }
        [Required(ErrorMessage = "Deal Name is required.")]
        [StringLength(50)]
        public string DealName { get; set; }
        [Required(ErrorMessage = "Deal Type is required.")]
        [StringLength(50)]
        public string DealType { get; set; }
        [Required(ErrorMessage = "Source List Id is required.")]
        [StringLength(50)]
        public string SourceListId { get; set; }
        [Required(ErrorMessage = "Side is required.")]
        [StringLength(50)]
        public string Side { get; set; }
    }
}
