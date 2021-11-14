﻿using immersed.dive.shop.model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace immersed.dive.shop.domain.interfaces
{
    public interface IEventService
    {
        Task<EventParticipant> GetEventParticipant(Guid eventId, Guid eventParticipantId);
        Task<Guid> AddParticipant(Guid eventId, Guid personId);
        Task<List<model.Person>> GetParticipants(Guid eventId);
    }
}