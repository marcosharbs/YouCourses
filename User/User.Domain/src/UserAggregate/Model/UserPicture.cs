using System.Collections.Generic;
using Core.Domain;

namespace User.Domain.UserAggregate.Model
{
    public class UserPicture : ValueObject<UserPicture>
    {
        public string ImageUrl { get; }

        protected UserPicture() {}

        public UserPicture(string imageUrl)
        {
            ImageUrl = imageUrl;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return ImageUrl;
        }
    }
}