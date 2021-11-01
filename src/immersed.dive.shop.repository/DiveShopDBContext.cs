using immersed.dive.shop.domain.interfaces;
using immersed.dive.shop.model;
using Microsoft.EntityFrameworkCore;

namespace immersed.dive.shop.repository
{
    public class DiveShopDBContext : DbContext
    {
        public DiveShopDBContext(DbContextOptions<DiveShopDBContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Course>(x => x.HasKey(k => k.Id));
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Person> Persons { get; set; }
    }
}
