using System;
using System.Collections.Generic;
using immersed.dive.shop.model;

namespace immersed.dive.shop.webapi.WebDtos
{
    public class CourseDto
    {
        public CourseDto()
        {
            Participants = new List<Person>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public string Description { get; set; }

        public List<Person> Participants { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool Live { get; set; }
    }
}