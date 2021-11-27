using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using immersed.dive.shop.model;
using Microsoft.EntityFrameworkCore;

namespace immersed.dive.shop.repository
{
    public static class Seed
    {
        public static async Task SeedData(this IServiceProvider servicesProvider, DiveShopDBContext context)
        {
            context.Database.ExecuteSqlRaw("DELETE FROM [EventDates]");
            context.Database.ExecuteSqlRaw("DELETE FROM [EventParticipants]");
            context.Database.ExecuteSqlRaw("DELETE FROM [Events]");
            context.Database.ExecuteSqlRaw("DELETE FROM [Courses]");
            context.Database.ExecuteSqlRaw("DELETE FROM [People]");

            context.Courses.Add(new Course() {Name = "Open Water", DateCreated = DateTime.UtcNow, LastUpdated = DateTime.UtcNow});
            context.Courses.Add(new Course() { Name = "Advanced Open Water", DateCreated = DateTime.UtcNow, LastUpdated = DateTime.UtcNow });
            context.Courses.Add(new Course() { Name = "Rescue Diver", DateCreated = DateTime.UtcNow, LastUpdated = DateTime.UtcNow });
            context.Courses.Add(new Course() { Name = "Dive Master", DateCreated = DateTime.UtcNow, LastUpdated = DateTime.UtcNow });
            context.SaveChanges();

            context.People.Add(new Person() {FamilyName = "Smith", Name = "Bob", IdentifiesAs = "Male", Sex=Sex.M, DateCreated = DateTime.UtcNow, LastUpdated = DateTime.UtcNow });
            context.People.Add(new Person() { FamilyName = "Jones", Name = "Mary", IdentifiesAs = "Male", Sex = Sex.F, DateCreated = DateTime.UtcNow, LastUpdated = DateTime.UtcNow });
            context.People.Add(new Person() { FamilyName = "Williams", Name = "Joan", IdentifiesAs = "Male", Sex = Sex.F, DateCreated = DateTime.UtcNow, LastUpdated = DateTime.UtcNow });
            context.People.Add(new Person() {FamilyName = "Castelan", Name = "Marcus", IdentifiesAs = "Male", Sex=Sex.M, DateCreated = DateTime.UtcNow, LastUpdated = DateTime.UtcNow });
            context.People.Add(new Person() { FamilyName = "Willis", Name = "Bruce", IdentifiesAs = "They/Them", Sex = Sex.I, DateCreated = DateTime.UtcNow, LastUpdated = DateTime.UtcNow });
            context.SaveChanges();

            context.Events.Add(new Event()
            {
                CourseId = context.Courses.AsQueryable().Single(c => c.Name == "Open Water").Id,
                Dates = new List<EventDate>()
                {
                    new () {Date = DateTime.UtcNow.AddDays(7), EstimatedDuration = 1, DateCreated = DateTime.UtcNow, LastUpdated = DateTime.UtcNow},
                    new () {Date = DateTime.UtcNow.AddDays(8), EstimatedDuration = 1, DateCreated = DateTime.UtcNow, LastUpdated = DateTime.UtcNow},
                    new () {Date = DateTime.UtcNow.AddDays(14), EstimatedDuration = 1, DateCreated = DateTime.UtcNow, LastUpdated = DateTime.UtcNow},
                    new () {Date = DateTime.UtcNow.AddDays(15), EstimatedDuration = 1, DateCreated = DateTime.UtcNow, LastUpdated = DateTime.UtcNow},
                },
                DateCreated = DateTime.UtcNow,
                LastUpdated = DateTime.UtcNow
            });

            context.Events.Add(new Event()
            {
                CourseId = context.Courses.AsQueryable().Single(c => c.Name == "Advanced Open Water").Id,
                Dates = new List<EventDate>()
                {
                    new () {Date = DateTime.UtcNow.AddDays(21), EstimatedDuration = 1, DateCreated = DateTime.UtcNow, LastUpdated = DateTime.UtcNow},
                    new () {Date = DateTime.UtcNow.AddDays(22), EstimatedDuration = 1, DateCreated = DateTime.UtcNow, LastUpdated = DateTime.UtcNow},
                },
                DateCreated = DateTime.UtcNow.AddDays(16),
                LastUpdated = DateTime.UtcNow.AddDays(16)
            });

            context.Events.Add(new Event()
            {
                CourseId = context.Courses.AsQueryable().Single(c => c.Name == "Rescue Diver").Id,
                Dates = new List<EventDate>()
                {
                    new () {Date = DateTime.UtcNow.AddDays(28), EstimatedDuration = 1, DateCreated = DateTime.UtcNow, LastUpdated = DateTime.UtcNow},
                    new () {Date = DateTime.UtcNow.AddDays(29), EstimatedDuration = 1, DateCreated = DateTime.UtcNow, LastUpdated = DateTime.UtcNow},
                },
                DateCreated = DateTime.UtcNow.AddDays(17),
                LastUpdated = DateTime.UtcNow.AddDays(17)
            });

            context.Events.Add(new Event()
            {
                CourseId = context.Courses.AsQueryable().Single(c => c.Name == "Dive Master").Id,
                Dates = new List<EventDate>()
                {
                    new () {Date = DateTime.UtcNow.AddDays(28), EstimatedDuration = 1, DateCreated = DateTime.UtcNow, LastUpdated = DateTime.UtcNow},
                    new () {Date = DateTime.UtcNow.AddDays(29), EstimatedDuration = 1, DateCreated = DateTime.UtcNow, LastUpdated = DateTime.UtcNow},
                    new () {Date = DateTime.UtcNow.AddDays(30), EstimatedDuration = 1, DateCreated = DateTime.UtcNow, LastUpdated = DateTime.UtcNow},
                    new () {Date = DateTime.UtcNow.AddDays(31), EstimatedDuration = 1, DateCreated = DateTime.UtcNow, LastUpdated = DateTime.UtcNow},
                    new () {Date = DateTime.UtcNow.AddDays(32), EstimatedDuration = 1, DateCreated = DateTime.UtcNow, LastUpdated = DateTime.UtcNow},
                    new () {Date = DateTime.UtcNow.AddDays(33), EstimatedDuration = 1, DateCreated = DateTime.UtcNow, LastUpdated = DateTime.UtcNow},
                },
                DateCreated = DateTime.UtcNow.AddDays(17),
                LastUpdated = DateTime.UtcNow.AddDays(17)
            });

            context.SaveChanges();

            context.EventParticipants.Add(new EventParticipant()
            {
                EventId = context.Events.Include(c=>c.Course).AsQueryable().Single(e => e.Course.Name == "Open Water").Id,
                ParticipantId = context.People.AsQueryable().Single(p => p.FamilyName == "Smith").Id
            });

            context.EventParticipants.Add(new EventParticipant()
            {
                EventId = context.Events.Include(c => c.Course).AsQueryable().Single(e => e.Course.Name == "Open Water").Id,
                ParticipantId = context.People.AsQueryable().Single(p => p.FamilyName == "Jones").Id
            });

            context.EventParticipants.Add(new EventParticipant()
            {
                EventId = context.Events.Include(c => c.Course).AsQueryable().Single(e => e.Course.Name == "Open Water").Id,
                ParticipantId = context.People.AsQueryable().Single(p => p.FamilyName == "Williams").Id
            });

            context.EventParticipants.Add(new EventParticipant()
            {
                EventId = context.Events.Include(c => c.Course).AsQueryable().Single(e => e.Course.Name == "Open Water").Id,
                ParticipantId = context.People.AsQueryable().Single(p => p.FamilyName == "Castelan").Id
            });

            context.EventParticipants.Add(new EventParticipant()
            {
                EventId = context.Events.Include(c => c.Course).AsQueryable().Single(e => e.Course.Name == "Advanced Open Water").Id,
                ParticipantId = context.People.AsQueryable().Single(p => p.FamilyName == "Jones").Id
            });

            context.EventParticipants.Add(new EventParticipant()
            {
                EventId = context.Events.Include(c => c.Course).AsQueryable().Single(e => e.Course.Name == "Advanced Open Water").Id,
                ParticipantId = context.People.AsQueryable().Single(p => p.FamilyName == "Williams").Id
            });

            context.EventParticipants.Add(new EventParticipant()
            {
                EventId = context.Events.Include(c => c.Course).AsQueryable().Single(e => e.Course.Name == "Advanced Open Water").Id,
                ParticipantId = context.People.AsQueryable().Single(p => p.FamilyName == "Castelan").Id
            });

            context.EventParticipants.Add(new EventParticipant()
            {
                EventId = context.Events.Include(c => c.Course).AsQueryable().Single(e => e.Course.Name == "Rescue Diver").Id,
                ParticipantId = context.People.AsQueryable().Single(p => p.FamilyName == "Williams").Id
            });

            context.EventParticipants.Add(new EventParticipant()
            {
                EventId = context.Events.Include(c => c.Course).AsQueryable().Single(e => e.Course.Name == "Rescue Diver").Id,
                ParticipantId = context.People.AsQueryable().Single(p => p.FamilyName == "Castelan").Id
            });

            context.EventParticipants.Add(new EventParticipant()
            {
                EventId = context.Events.Include(c => c.Course).AsQueryable().Single(e => e.Course.Name == "Dive Master").Id,
                ParticipantId = context.People.AsQueryable().Single(p => p.FamilyName == "Willis").Id
            });

            context.SaveChanges();

        }
    }
}
