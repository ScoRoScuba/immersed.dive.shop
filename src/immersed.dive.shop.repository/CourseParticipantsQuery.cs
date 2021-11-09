using System;
using System.Collections.Generic;
using System.Linq;
using immersed.dive.shop.domain.interfaces.Data;
using immersed.dive.shop.model;
using Microsoft.EntityFrameworkCore;

namespace immersed.dive.shop.repository
{
    public class CourseParticipantsQuery : ICriteria<CourseParticipant>
    {
        private readonly Guid _courseId;

        public CourseParticipantsQuery(Guid courseId )
        {
            _courseId = courseId;
        }

        public IList<CourseParticipant> MatchQueryFrom(IQueryable<CourseParticipant> ds)
        {
            var result = ds
                .Include(p => p.Participant)
                .Include( c =>c.Course)
                .Where(cp => cp.CourseId == _courseId).ToList();

            return result.ToList();
        }
    }
}
