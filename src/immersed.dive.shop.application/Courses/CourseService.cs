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

        public CourseService(IDataStore<Course> courseDataStore, IPersonService personService)
        {
            _courseDataStore = courseDataStore;
            _personService = personService;
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

        public async Task<int> AddParticipant(Guid courseId, Guid personId)
        {
            var course = await _courseDataStore.FindAsync(c => c.Id == courseId);

            var person = await _personService.Get(personId);

            course.Participants.Add(new CourseParticipant
            {
                CourseId = courseId,
                Course = course,
                ParticipantId = personId,
                Participant = person
            });

            var result = await _courseDataStore.UpdateAsync(course);

            return course.Participants.Count;
        }
    }
}
