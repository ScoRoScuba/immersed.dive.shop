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
            modelBuilder.Entity<Person>(x => x.HasKey(k => k.Id));

            modelBuilder.Entity<CourseParticipant>(x => x.HasKey(cp => new { cp.CourseId, cp.ParticipantId }));

            modelBuilder.Entity<CourseParticipant>()
                .HasOne(u => u.Participant)
                .WithMany(a => a.Courses)
                .HasForeignKey(aa => aa.ParticipantId);

            modelBuilder.Entity<CourseParticipant>()
                .HasOne(u => u.Course)
                .WithMany(a => a.Participants)
                .HasForeignKey(aa => aa.CourseId);
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Person> Persons { get; set; }

        public DbSet<CourseParticipant> CourseParticipants{ get; set; }
    }
}
