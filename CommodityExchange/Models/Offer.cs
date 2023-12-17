using CommodityExchange.Enums;

namespace CommodityExchange.Models
{
    public class Offer
    {
        public int Id { get; set; }
        public string OfferorId { get; set; }
        public User Offeror { get; set; }
        public int BarterId { get; set; }
        public Barter Barter { get; set; }
        public decimal OfferPrice { get; set; }
        public OfferStatus Status { get; set; }
    }
}
