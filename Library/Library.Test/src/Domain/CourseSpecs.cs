using System;
using Xunit;
using FluentAssertions;
using Library.Domain.CourseAggregate.Model;
using Library.Domain.AuthorAggregate.Model;
using System.Linq;

namespace Library.Test.Domain
{
    public class CourseSpecs
    {
        private Author author = Author.Create("Marcos Harbs", "http://marcos.harbs.com/imagem.jpeg");

        [Fact]
        public void Two_courses_same_id_are_equals()
        {
            var id = Guid.NewGuid();
        
            var course1 = Course.Create(id, "Curso de JS", "Curso para pessoas que querem estudar JS", author);
            var course2 = Course.Create(id, "Curso de JAVASCRIPT", "Curso para pessoas que querem estudar JS", author);
            
            course1.Should().Be(course2);
            course1.GetHashCode().Should().Be(course2.GetHashCode());
        }

        [Fact]
        public void Two_courses_different_id_are_not_equals()
        {
            var course1 = Course.Create("Curso de JS", "Curso para pessoas que querem estudar JS", author);
            var course2 = Course.Create("Curso de JS", "Curso para pessoas que querem estudar JS", author);
            
            course1.Should().NotBe(course2);
            course1.GetHashCode().Should().NotBe(course2.GetHashCode());
        }

        [Fact]
        public void Cannot_create_course_id_empty()
        {
            Action action = () => Course.Create(Guid.Empty, "Curso de JS", "Curso para pessoas que querem estudar JS", author);
            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Cannot_create_course_name_empty()
        {
            Action action = () => Course.Create("", "Curso para pessoas que querem estudar JS", author);
            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Cannot_create_course_description_empty()
        {
            Action action = () => Course.Create("Curso de JS", "", author);
            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Cannot_create_course_author_null()
        {
            Action action = () => Course.Create("Curso de JS", "Curso para pessoas que querem estudar JS", null);
            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Cannot_create_course_videos_null_if_informed()
        {
            Action action = () => Course.Create("Curso de JS", "Curso para pessoas que querem estudar JS", author, null);
            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Cannot_create_course_description_less_trithy_characteres()
        {
            Action action = () => Course.Create("Curso de JS", "12345678912345678912345678901", author);
            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Cannot_create_course_description_more_four_hundred_characteres()
        {
            var description = "1";

            for(int i = 0; i<40; i++)
            {
                description += "1234567890";
            }

            Action action = () => Course.Create("Curso de JS", description, author);
            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void After_update_course_name_should_return_correct_name()
        {
            var course = Course.Create("Curso de JS", "Curso para pessoas que querem estudar JS", author);
            course.UpdateName("Novo curso de JS");
            course.CourseName.Name.Should().Be("Novo curso de JS");
        }

        [Fact]
        public void After_update_course_description_should_return_correct_description()
        {
            var course = Course.Create("Curso de JS", "Curso para pessoas que querem estudar JS", author);
            course.UpdateDescription("Curso para pessoas que querem estudar JS atualizado");
            course.CourseDescription.Description.Should().Be("Curso para pessoas que querem estudar JS atualizado");
        }

        [Fact]
        public void After_add_videos_must_return_same_videos()
        {
            var video1 = Video.Create("curso_1", "http://curso1.com");
            var video2 = Video.Create("curso_2", "http://curso2.com");
            var video3 = Video.Create("curso_3", "http://curso3.com");
            var video4 = Video.Create("curso_4", "http://curso4.com");
            var video5 = Video.Create("curso_5", "http://curso5.com");

            var course = Course.Create("Curso de JS", "Curso para pessoas que querem estudar JS", author);
            course.AddVideo(video1);
            course.AddVideo(video2);
            course.AddVideo(video3);
            course.AddVideo(video4);
            course.AddVideo(video5);

            course.Videos.ElementAt(0).Should().Be(video1);
            course.Videos.ElementAt(1).Should().Be(video2);
            course.Videos.ElementAt(2).Should().Be(video3);
            course.Videos.ElementAt(3).Should().Be(video4);
            course.Videos.ElementAt(4).Should().Be(video5);
        }

        [Fact]
        public void Cannot_add_two_videos_same_url()
        {
            Action action = () => {
                var course = Course.Create( "Curso de JS", "Curso para pessoas que querem estudar JS", author);
                course.AddVideo(Video.Create("curso_1", "http://curso1.com"));
                course.AddVideo(Video.Create("curso_2", "http://curso2.com"));
                course.AddVideo(Video.Create("curso_3", "http://curso3.com"));
                course.AddVideo(Video.Create("curso_4", "http://curso4.com"));
                course.AddVideo(Video.Create("curso_5", "http://curso4.com"));
            };

            action.Should().Throw<InvalidOperationException>();
        }

    }
}
