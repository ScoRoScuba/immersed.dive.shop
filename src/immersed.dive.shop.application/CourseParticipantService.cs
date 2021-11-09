using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using immersed.dive.shop.domain.interfaces;
using immersed.dive.shop.domain.interfaces.Data;
using immersed.dive.shop.model;
using immersed.dive.shop.repository;

namespace immersed.dive.shop.application
{
    public class CourseParticipantService : ICourseParticipantService
    {
        private readonly IDataStore<CourseParticipant> _courseParticipantDataStore;

        public CourseParticipantService(IDataStore<CourseParticipant> courseParticipantDataStore)
        {
            _courseParticipantDataStore = courseParticipantDataStore;
        }

        public async Task<List<model.Person>> GetCourseParticipants(Guid courseId)
        {

            var result = await _courseParticipantDataStore.MatchAsync(new CourseParticipantsQuery(courseId));

            if( result.Any()){
                return result.Select(cp => cp.Participant).ToList();
            }

            return new List<model.Person>();
        }
    }
}
