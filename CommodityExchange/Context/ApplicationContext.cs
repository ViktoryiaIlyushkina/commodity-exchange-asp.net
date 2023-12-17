using CommodityExchange.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CommodityExchange.Context
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base (options) { }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Barter> Barters { get; set; }
        public virtual DbSet<Offer> Offers { get; set; }


    }
}
