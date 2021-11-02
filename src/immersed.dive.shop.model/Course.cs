using System;
using System.Collections.Generic;

namespace immersed.dive.shop.model
{
    public class Course : IEntity
    {
        public Course()
        {
            Id = Guid.NewGuid();

            People = new List<Person>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }

        public List<Person> People { get; set; }
    }
}