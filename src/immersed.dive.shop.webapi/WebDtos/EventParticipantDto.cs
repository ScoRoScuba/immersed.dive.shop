using System;
using immersed.dive.shop.model;

namespace immersed.dive.shop.webapi.WebDtos
{
    public class EventParticipantDto
    {
        public EventParticipantDto()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public Guid EventId { get; set; }
        public Event Event { get; set; }

        public Guid ParticipantId { get; set; }
        public PersonDto Participant { get; set; }

        public DateTime DateRegistered { get; set; }
        public DateTime DateConfirmed { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool Live { get; set; }

    }
}
