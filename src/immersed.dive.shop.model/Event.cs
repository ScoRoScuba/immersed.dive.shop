using System;
using System.Collections.Generic;

namespace immersed.dive.shop.model;

public class Event : IEntity
{
    public Event()
    {
        Participants = new List<EventParticipant>();
        Dates = new List<EventDate>();
    }

    public virtual List<EventDate> Dates { get; set; }

    public Guid  CourseId { get; set; }
    public virtual Course Course { get; set; }

    public virtual List<EventParticipant> Participants { get; set; }

    public Guid Id { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime LastUpdated { get; set; }
    public bool Live { get; set; }
}