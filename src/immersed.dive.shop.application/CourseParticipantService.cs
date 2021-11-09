using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using immersed.dive.shop.domain.interfaces;
using immersed.dive.shop.domain.interfaces.Data;
using immersed.dive.shop.model;
using immersed.dive.shop.repository;
using Serilog;

namespace immersed.dive.shop.application
{
    public class CourseParticipantService : ICourseParticipantService
    {
        private readonly IDataStore<CourseParticipant> _courseParticipantDataStore;
        private readonly ILogger _logger;

        public CourseParticipantService(IDataStore<CourseParticipant> courseParticipantDataStore, ILogger logger)
        {
            _courseParticipantDataStore = courseParticipantDataStore;
            _logger = logger;
        }

        public async Task<List<model.Person>> GetCourseParticipants(Guid courseId)
        {
            var result = await _courseParticipantDataStore.MatchAsync(new CourseParticipantsQuery(courseId));

            if( result.Any()){
                return result.Select(cp => cp.Participant).ToList();
            }

            return new List<model.Person>();
        }

        public async Task<CourseParticipant> GetCourseParticipant(Guid courseParticipantId)
        {
            var result = await _courseParticipantDataStore.FindAsync(cp=>cp.Id == courseParticipantId);

            return result;

        }
    }
}
