using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(StoreContext options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}