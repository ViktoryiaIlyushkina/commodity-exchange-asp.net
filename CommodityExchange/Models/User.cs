using Microsoft.AspNetCore.Identity;

namespace CommodityExchange.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

    }
}
