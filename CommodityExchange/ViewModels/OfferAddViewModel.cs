using CommodityExchange.Enums;
using CommodityExchange.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CommodityExchange.ViewModels
{
    public class OfferAddViewModel
    {
        [Required]
        public int BarterId { get; set; }

        [Required]
        [Display(Name = "Предмет аукциона")]
        public string BarterName { get; set; }

        [Required]
        [Display(Name = "Стартовая цена")]
        public decimal StartPrice { get; set; }

        [Required]
        public string OfferorId { get; set; }

        [Required]
        [Display(Name = "Оферент")]
        public string Offeror { get; set; }

        [Required]
        [Display(Name = "Цена оферты")]
        public decimal OfferPrice { get; set; }

        [Required]
        public OfferStatus Status { get; set; }
    }
}
