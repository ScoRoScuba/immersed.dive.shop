using immersed.dive.shop.domain.interfaces;
using immersed.dive.shop.model;
using Microsoft.EntityFrameworkCore;

namespace immersed.dive.shop.repository;

public class DiveShopDBMigrationsContext : DbContext
{
    public DiveShopDBMigrationsContext(DbContextOptions<DiveShopDBMigrationsContext> options) : base(options)
    {
            
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Course>(x => x.HasKey(k => k.Id));
        modelBuilder.Entity<Event>(x => x.HasKey(k => k.Id));
        modelBuilder.Entity<Person>(x => x.HasKey(k => k.Id));

        modelBuilder.Entity<EventParticipant>(x => x.HasKey(cp => new { cp.EventId, cp.ParticipantId }));

        modelBuilder.Entity<EventParticipant>()
            .HasOne(u => u.Participant)
            .WithMany(a => a.Events)
            .HasForeignKey(aa => aa.ParticipantId);

        modelBuilder.Entity<EventParticipant>()
            .HasOne(u => u.Event)
            .WithMany(a => a.Participants)
            .HasForeignKey(aa => aa.EventId);
    }

    public DbSet<Course> Courses { get; set; }
    public DbSet<Person> People { get; set; }

    public DbSet<EventDate> EventDates{ get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<EventParticipant> EventParticipants{ get; set; }
}