using System.Collections.Generic;

namespace Library.RestApi.Model
{
    public class Author
    {
        public string Name { get; }
        public string ImageUrl { get; }

        public Author(string name, string imageUrl)
        {
            Name = name;
            ImageUrl = imageUrl;
        }

        public static Author From(Library.Domain.AuthorAggregate.Model.Author author)
        {
            return new Author(author.AuthorName.Name, author.AuthorPicture.ImageUrl);
        }

        public static IEnumerable<Author> From(IEnumerable<Library.Domain.AuthorAggregate.Model.Author> authors)
        {
            var list = new List<Author>();

            foreach (var author in authors)
            {
                list.Add(Author.From(author));
            }

            return list;
        }
    }
}