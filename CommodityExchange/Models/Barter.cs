using CommodityExchange.Enums;

namespace CommodityExchange.Models
{
    public class Barter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[]? Photo { get; set; }
        public StatusType Status { get; set; }
        public string Owner { get; set; }
        public string Description { get; set; }
        public decimal StartPrice { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? ClosedTime { get; set; }
        public List<Offer> Offers { get; set; }

    }
}
