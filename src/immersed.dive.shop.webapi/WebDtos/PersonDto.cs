using System;
using System.Collections.Generic;
using immersed.dive.shop.model;

namespace immersed.dive.shop.webapi.WebDtos
{
    public class PersonDto
    {
        public PersonDto()
        {
            Courses = new List<CourseDto>();
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

        public List<CourseDto> Courses { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool Live { get; set; }
    }
}
