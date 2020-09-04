using Microsoft.AspNetCore.Mvc;
using Library.Application;
using Library.Domain.Common;

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
        public Library.RestApi.Model.PaginatedData<Library.RestApi.Model.Course> Get(int page = 0)
        {
            var totalCourses = new GetTotalCoursesUseCase(_unitOfWork).Execute();
            var courses = new GetCoursesUseCase(page, _unitOfWork).Execute();
            return Library.RestApi.Model.PaginatedData<Library.RestApi.Model.Course>.Create(totalCourses, page, Library.RestApi.Model.Course.From(courses));
        }
    }
}
