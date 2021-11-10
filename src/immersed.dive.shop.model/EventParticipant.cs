using System;

namespace immersed.dive.shop.model
{
    public class EventParticipant : IEntity
    {
        public EventParticipant()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public Guid EventId { get; set; }
        public Event Event { get; set; }
        
        public Guid ParticipantId { get; set; }
        public Person Participant { get; set; }

        public DateTime DateRegistered { get; set; }
        public DateTime DateConfirmed { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool Live { get; set; }
    }
}
