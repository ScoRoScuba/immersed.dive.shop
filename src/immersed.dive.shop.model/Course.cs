using System;
using System.Collections.Generic;

namespace immersed.dive.shop.model
{
    public class Course : IEntity
    {
        public Course()
        {
            Id = Guid.NewGuid();

            Events = new List<Event>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Event> Events{ get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool Live { get; set; }
    }
}