using System;
using System.Collections;
using System.Collections.Generic;

namespace immersed.dive.shop.model
{
    public class Person : IEntity
    {
        public Person()
        {
            Id = Guid.NewGuid();
            Courses = new List<CourseParticipant>();
        }

        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string FamilyName { get; set; }
        public string Name { get; set; }
        public string OtherNames { get; set; }
        public string KnownAs { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Sex Sex { get; set; }
        public string IdentifiesAs { get; set; }

        public List<CourseParticipant> Courses { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool Live { get; set; }
    }
}