using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using immersed.dive.shop.domain.interfaces;
using immersed.dive.shop.domain.interfaces.Data;
using immersed.dive.shop.model;
using immersed.dive.shop.repository;
using immersed.dive.shop.repository.Criteria;
using Serilog;

namespace immersed.dive.shop.application;

public class EventParticipantService : IEventParticipantService
{
    private readonly IDataStore<EventParticipant> _eventParticipantDataStore;
    private readonly ILogger _logger;

    public EventParticipantService(IDataStore<EventParticipant> eventParticipantDataStore, ILogger logger)
    {
        _eventParticipantDataStore = eventParticipantDataStore;
        _logger = logger;
    }

    public async Task<List<model.Person>> GetParticipants(Guid courseId)
    {
        var result = await _eventParticipantDataStore.MatchAsync(new EventParticipantsCriteria(courseId));

        if( result.Any()){
            return result.Select(cp => cp.Participant).ToList();
        }

        return new List<model.Person>();
    }

    public async Task<EventParticipant> GetParticipant(Guid eventParticipantId)
    {
        var result = await _eventParticipantDataStore.MatchAsync(new GetEventParticipantCriteria(eventParticipantId));

        return result.FirstOrDefault();
    }
}