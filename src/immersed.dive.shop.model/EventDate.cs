using System;

namespace immersed.dive.shop.model
{
    public class EventDate : IEntity
    {
        public EventDate()
        {
            
        }

        public DateTime Date { get; set; }
        public int EstimatedDuration { get; set; }
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool Live { get; set; }
    }
}
