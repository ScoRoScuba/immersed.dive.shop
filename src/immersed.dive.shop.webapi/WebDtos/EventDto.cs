using System;
using System.Collections.Generic;

namespace immersed.dive.shop.webapi.WebDtos;

public class EventDto
{
    public EventDto()
    {
        Participants = new List<EventParticipantDto>();
        Dates = new List<EventDateDto>();
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public string Description { get; set; }

    public List<EventParticipantDto> Participants { get; set; }
    public List<EventDateDto> Dates { get; set; }

    public DateTime DateCreated { get; set; }
    public DateTime LastUpdated { get; set; }
    public bool Live { get; set; }

    public Guid CourseId { get; set; }
    public CourseDto Course { get; set; }
}

public class EventDateDto
{
    public DateTime Date { get; set; }
    public int EstimatedDuration { get; set; }
    public Guid Id { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime LastUpdated { get; set; }
    public bool Live { get; set; }
}