using System;
using Xunit;
using FluentAssertions;
using Library.Domain.CourseAggregate.Model;

namespace Library.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Two_video_instances_equal_if_same_name_and_url()
        {
            Video video1 = new Video("O que o JavaScript é capaz de fazer?", "https://www.youtube.com/watch?v=Ptbk2af68e8");
            Video video2 = new Video("O que o JavaScript é capaz de fazer?", "https://www.youtube.com/watch?v=Ptbk2af68e8");
            
            video1.Should().Be(video2);
            video1.GetHashCode().Should().Be(video2.GetHashCode());
        }

        [Fact]
        public void Two_video_instances_do_not_equal_if_not_same_name_or_url()
        {
            Video video1 = new Video("O que o JavaScript é capaz de fazer?", "https://www.youtube.com/watch?v=Ptbk2af68e8");
            Video video2 = new Video("JavaScript: como chegamos até aqui?", "https://www.youtube.com/watch?v=rUTKomc2gG8");
            
            video1.Should().NotBe(video2);
            video1.GetHashCode().Should().NotBe(video2.GetHashCode());
        }

        [Fact]
        public void Cannot_create_video_name_less_five_characters()
        {
            Action action = () => new Video("Java", "https://www.youtube.com/watch?v=rUTKomc2gG8");
            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Cannot_create_video_url_empty()
        {
            Action action = () => new Video("O que o JavaScript é capaz de fazer?", "");
            action.Should().Throw<ArgumentException>();
        }
    }
}
