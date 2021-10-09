using System;

namespace immersed.dive.shop.model
{
    public class Course : IEntity
    {
        public Course()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public string Name { get; set; }
        public DateTime StartDate { get; set; }

        
    }
}