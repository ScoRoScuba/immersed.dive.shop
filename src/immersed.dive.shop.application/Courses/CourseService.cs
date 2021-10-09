using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using immersed.dive.shop.domain.interfaces;
using immersed.dive.shop.domain.interfaces.Data;
using immersed.dive.shop.model;

namespace immersed.dive.shop.application.Courses
{
    public class CourseService : ICourseService
    {
        private readonly IDataStore<Course> _courseDataStore;

        public CourseService( IDataStore<Course> courseDataStore)
        {
            _courseDataStore = courseDataStore;
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
    }
}
