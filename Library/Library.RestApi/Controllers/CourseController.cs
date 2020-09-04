using System.Collections.Generic;
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
        public IEnumerable<Library.RestApi.Model.Course> Get()
        {
            var courses = new GetCoursesUseCase(_unitOfWork).Execute();
            return Library.RestApi.Model.Course.From(courses);
        }
    }
}
