using System.Collections.Generic;
using Core.Domain;

namespace User.Domain.UserAggregate.Model
{
    public class UserEmail : ValueObject<UserEmail>
    {
        public string Email { get; }

        protected UserEmail() {}

        public UserEmail(string email)
        {
            Email = email;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Email;
        }
    }
}