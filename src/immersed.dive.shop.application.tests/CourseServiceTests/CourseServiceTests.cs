﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using immersed.dive.shop.domain.interfaces.Data;
using immersed.dive.shop.model;
using immersed.dive.shop.application.Courses;
using immersed.dive.shop.domain.interfaces;
using Moq;
using Serilog;
using Xunit;

namespace immersed.dive.shop.application.tests.CourseServiceTests;

public class CourseServiceTests
{
    private Mock<ILogger> mockLogger = new Mock<ILogger>();

    [Fact]
    public async Task GetAllReturnsAllCourses()
    {
        var mockDataStore = new Mock<IDataStore<Course>>();

        mockDataStore.Setup( d=>d.GetAllAsync()).ReturnsAsync(()=> new List<Course>
        {
            new(),
            new(),
            new()
        });

        var courseService = new CourseService(mockDataStore.Object, mockLogger.Object);

        var result = await courseService.GetAll();

        Assert.NotEmpty(result);
        Assert.True(result.Count == 3);
    }

    [Fact]
    public async Task GetByIdReturnsCourseThatExists()
    {
        var mockDataStore = new Mock<IDataStore<Course>>();

        var candidateGuid = Guid.NewGuid();

        var list = new List<Course>
        {
            new Course() {Id = candidateGuid},
            new Course(),
            new Course()
        };

        mockDataStore.Setup(d => d.FindAsync(It.IsAny<Expression<Func<Course, bool>>>())).ReturnsAsync(
            (Expression<Func<Course, bool>> predicate) => list.AsQueryable().Single(predicate));

        var courseService = new CourseService(mockDataStore.Object, mockLogger.Object);

        var result = await courseService.Get(candidateGuid);

        Assert.NotNull(result);
        Assert.True( result.Id == candidateGuid);
    }

    [Fact]
    public async Task GetByIdReturnsCourseDoesNotExist()
    {
        var mockDataStore = new Mock<IDataStore<Course>>();

        var candidateGuid = Guid.NewGuid();

        var list = new List<Course>
        {
            new Course() {Id = candidateGuid},
            new Course(),
            new Course()
        };

        mockDataStore.Setup(d => d.FindAsync(It.IsAny<Expression<Func<Course, bool>>>())).ReturnsAsync(
            (Expression<Func<Course, bool>> predicate) => list.AsQueryable().SingleOrDefault(predicate));

        var courseService = new CourseService(mockDataStore.Object, mockLogger.Object);

        var result = await courseService.Get(Guid.NewGuid());

        Assert.Null(result);
    }
}