using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using immersed.dive.shop.domain.interfaces;
using immersed.dive.shop.domain.interfaces.Data;
using immersed.dive.shop.model;

namespace immersed.dive.shop.application.Courses
{
    public class CourseService : ICourseService
    {
        private readonly IDataStore<Course> _courseDataStore;
        private readonly IPersonService _personService;
        private readonly ICourseParticipantService _courseParticipantService;

        public CourseService(IDataStore<Course> courseDataStore, IPersonService personService, ICourseParticipantService courseParticipantService)
        {
            _courseDataStore = courseDataStore;
            _personService = personService;
            _courseParticipantService = courseParticipantService;
        }

        public async Task<Course> Get(Guid id)
        {
            return await _courseDataStore.FindAsync(c => c.Id == id);
        }

        public async Task Add(Course course)
        {
            await _courseDataStore.AddAsync(course);
        }

        public async Task<IList<Course>> GetAll()
        {
            return await _courseDataStore.GetAllAsync();
        }

        public async Task<Guid> AddParticipant(Guid courseId, Guid personId)
        {
            var course = await _courseDataStore.FindAsync(c => c.Id == courseId);

            var courseParticipant = new CourseParticipant
            {
                CourseId = courseId,
                ParticipantId = personId,
                Live = true,
                DateCreated = DateTime.UtcNow
            };

            course.Participants.Add(courseParticipant);

            var result = await _courseDataStore.UpdateAsync(course);

            return courseParticipant.Id;
        }

        public async Task<List<model.Person>> GetParticipants(Guid courseId)
        {
            var result = await _courseParticipantService.GetCourseParticipants(courseId);

            return result;
        }

        public async Task<CourseParticipant> GetCourseParticipant(Guid courseId, Guid courseParticipantId)
        {
            var course = await _courseDataStore.FindAsync(c => c.Id == courseId);

            if (course == null) return null;

            var result = await _courseParticipantService.GetCourseParticipant(courseParticipantId);

            return result;
        }
    }

}
