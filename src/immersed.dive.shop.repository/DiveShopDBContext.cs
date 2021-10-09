using immersed.dive.shop.model;
using Microsoft.EntityFrameworkCore;

namespace immersed.dive.shop.repository
{
    public class DiveShopDBContext : DbContext
    {
        public DiveShopDBContext(DbContextOptions<DiveShopDBContext> options) : base(options)
        {
            
        }

        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
