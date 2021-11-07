using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using immersed.dive.shop.model;

namespace immersed.dive.shop.domain.interfaces
{
    public interface ICourseParticipantService
    {
        Task<List<Person>> GetCourseParticipants(Guid courseId);
    }
}