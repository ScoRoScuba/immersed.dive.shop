using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using immersed.dive.shop.model;

namespace immersed.dive.shop.domain.interfaces
{
    public interface IEventParticipantService
    {
        Task<List<Person>> GetParticipants(Guid courseId);

        Task<EventParticipant> GetParticipant(Guid eventParticipantId);
    }
}