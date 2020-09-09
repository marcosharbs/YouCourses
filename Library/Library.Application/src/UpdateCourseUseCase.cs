using Library.Domain.Common;
using Library.Domain.CourseAggregate.Model;

namespace Library.Application
{
    public class UpdateCourseUseCase : UseCase<Course>
    {
        private readonly Course _course;

        public UpdateCourseUseCase(Course course,
                                   IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _course = course;
        }

        protected override Course Action()
        {
            var courses = _unitOfWork.Courses;
        
            var course = courses.GetById(_course.Id);
            
            course.RemoveVideos();

            foreach (var video in _course.Videos)
            {
                course.AddVideo(video);
            }

            course.UpdateName(_course.CourseName.Name);
            course.UpdateDescription(_course.CourseDescription.Description);

            courses.AddOrUpdate(course);

            return course;
        }
    }
}