using System;
using System.Collections.Generic;

namespace immersed.dive.shop.model
{
    public class Course : IEntity
    {
        public Course()
        {
            Id = Guid.NewGuid();

            Participants = new List<CourseParticipant>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }

        public List<CourseParticipant> Participants { get; set; }

    }
}