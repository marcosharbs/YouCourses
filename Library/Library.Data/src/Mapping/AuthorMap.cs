using FluentNHibernate.Mapping;
using Library.Domain.AuthorAggregate.Model;

namespace Library.Data.Mapping
{
    public class AuthorMap : ClassMap<Author>
    {
        public AuthorMap()
        {
            Id(author => author.Id).GeneratedBy.Assigned();

            Component(author => author.AuthorName, author => {
                author.Map(authorName => authorName.Name).Length(255);
            });

            Component(author => author.AuthorPicture, author => {
                author.Map(authorPicture => authorPicture.ImageUrl).Length(400);
            });
        }
    }
}