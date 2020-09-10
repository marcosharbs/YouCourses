using Microsoft.AspNetCore.Mvc;
using Library.Application;
using Library.Domain;
using Library.RestApi.Model;

namespace Library.RestApi.Controllers
{
    [ApiController]
    [Route("api/course")]
    public class CourseController : ControllerBase
    {
        private readonly ILibraryUnitOfWork _unitOfWork;

        public CourseController(ILibraryUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public PaginatedData<CourseApi> Get(int page = 0, int pageSize = 20)
        {
            var totalCourses = new GetTotalCoursesUseCase(_unitOfWork).Execute();
            var courses = new GetCoursesUseCase(page, pageSize, _unitOfWork).Execute();
            return PaginatedData<CourseApi>.Create(totalCourses, page, pageSize, CourseApi.FromDomain(courses));
        }

        [HttpPost]
        public CourseApi Post(CourseApi courseApi)
        {
            var course = CourseApi.ToDomain(courseApi);
            var newCourse = new SaveCourseUseCase(course, _unitOfWork).Execute();
            return CourseApi.FromDomain(newCourse);
        }

        [HttpPut]
        public CourseApi Put(CourseApi courseApi)
        {
            var course = CourseApi.ToDomain(courseApi);
            var updatedCourse = new UpdateCourseUseCase(course, _unitOfWork).Execute();
            return CourseApi.FromDomain(updatedCourse);
        }
    }
}
