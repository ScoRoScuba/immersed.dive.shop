﻿using System.Collections.Generic;
using immersed.dive.shop.domain.interfaces.Data;
using immersed.dive.shop.model;
using Moq;
using Xunit;

namespace immersed.dive.shop.application.tests.CourseServiceTests
{
    public class CourseParticipantsServiceTests
    {
        [Fact]
        public async void GetCourseParticipantsReturnsListOfPersonsOnCourse()
        {
            var mockDataStore = new Mock<IDataStore<CourseParticipant>>();

            var list = new List<CourseParticipant>()
            {
                new CourseParticipant()
                {
                    Course = new Course(),
                    Participant = new model.Person()
                },
                new CourseParticipant()
                {
                    Course = new Course(),
                    Participant = new model.Person()
                }
            };

            var courseId = list[0].Course.Id;
            list[0].CourseId = courseId;
            list[1].CourseId = courseId;
            list[1].Course.Id = courseId;

            mockDataStore.Setup(d => d.MatchAsync(It.IsAny<ICriteria<CourseParticipant>>())).ReturnsAsync(list);
            
            var courseParticipantService = new CourseParticipantService(mockDataStore.Object);

            var result = await courseParticipantService.GetCourseParticipants(courseId);

            Assert.NotEmpty(result);
            Assert.IsType<List<model.Person>>(result);
        }

    }
}