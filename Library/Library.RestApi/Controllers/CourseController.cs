﻿using Microsoft.AspNetCore.Mvc;
using Library.Application;
using Library.Domain.Common;
using Library.RestApi.Model;

namespace Library.RestApi.Controllers
{
    [ApiController]
    [Route("api/course")]
    public class CourseController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public PaginatedData<CourseApi> Get(int page = 0, int pageSize = 20)
        {
            var totalCourses = new GetTotalCoursesUseCase(_unitOfWork).Execute();
            var courses = new GetCoursesUseCase(page, pageSize, _unitOfWork).Execute();
            return Library.RestApi.Model.PaginatedData<CourseApi>.Create(totalCourses, page, pageSize, CourseApi.From(courses));
        }

        [HttpPost]
        public CourseApi Post(CourseApi courseApi)
        {
            var course = CourseApi.To(courseApi);
            var newCourse = new SaveCourseUseCase(course, _unitOfWork).Execute();
            return CourseApi.From(newCourse);
        }

        [HttpPut]
        public CourseApi Put(CourseApi courseApi)
        {
            var course = CourseApi.To(courseApi);
            var updatedCourse = new UpdateCourseUseCase(course, _unitOfWork).Execute();
            return CourseApi.From(updatedCourse);
        }
    }
}
