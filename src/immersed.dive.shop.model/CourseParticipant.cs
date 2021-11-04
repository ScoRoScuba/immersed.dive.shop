using System;

namespace immersed.dive.shop.model
{
    public class CourseParticipant
    {
        public Guid CourseId { get; set; }
        public Guid ParticipantId { get; set; }
        public Course Course { get; set; }
        public Person Participant { get; set; }
    }
}
