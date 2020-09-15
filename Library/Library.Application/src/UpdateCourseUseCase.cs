using Library.Domain;
using Library.Domain.CourseAggregate.Model;
using Core.Application;

namespace Library.Application
{
    public class UpdateCourseUseCase : UseCase<Course, LibraryUnitOfWork>
    {
        private readonly Course _course;

        public UpdateCourseUseCase(Course course,
                                   LibraryUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _course = course;
        }

        protected override Course Action()
        {
            var courses = _unitOfWork.GetCourseRepository();
        
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