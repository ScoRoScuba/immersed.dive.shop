using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using immersed.dive.shop.model;

namespace immersed.dive.shop.domain.interfaces
{
    public interface ICourseParticipantService
    {
        Task<List<CourseParticipant>> GetCourseParticipants(Guid courseId);
    }
}