using System.Collections.Generic;
using Core.Domain;

namespace Library.Domain.AuthorAggregate.Model
{
    public class AuthorPicture : ValueObject<AuthorPicture>
    {
        public string ImageUrl { get; }

        protected AuthorPicture() {}

        public AuthorPicture(string imageUrl)
        {
            ImageUrl = imageUrl;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return ImageUrl;
        }
    }
}