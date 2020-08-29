using System;
using Xunit;
using FluentAssertions;
using Library.Domain.CourseAggregate.Model;

namespace Library.Test.Domain
{
    public class VideoSpecs
    {
        [Fact]
        public void Two_video_instances_equal_if_same_id()
        {
            var id = Guid.NewGuid();

            var video1 = Video.Create(id, "O que o JavaScript é capaz de fazer?", "https://www.youtube.com/watch?v=Ptbk2af68e8");
            var video2 = Video.Create(id, "O que o JavaScript é?", "https://www.youtube.com/watch?v=Ptbk2af68e8");
            
            video1.Should().Be(video2);
            video1.GetHashCode().Should().Be(video2.GetHashCode());
        }

        [Fact]
        public void Two_video_instances_do_not_equal_if_not_same_id()
        {
            var video1 = Video.Create("O que o JavaScript é capaz de fazer?", "https://www.youtube.com/watch?v=Ptbk2af68e8");
            var video2 = Video.Create("O que o JavaScript é capaz de fazer?", "https://www.youtube.com/watch?v=Ptbk2af68e8");
            
            video1.Should().NotBe(video2);
            video1.GetHashCode().Should().NotBe(video2.GetHashCode());
        }

        [Fact]
        public void Cannot_create_video_name_less_five_characters()
        {
            Action action = () => Video.Create("Java", "https://www.youtube.com/watch?v=rUTKomc2gG8");
            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Cannot_create_video_url_empty()
        {
            Action action = () => Video.Create("O que o JavaScript é capaz de fazer?", "");
            action.Should().Throw<ArgumentException>();
        }
    }
}
