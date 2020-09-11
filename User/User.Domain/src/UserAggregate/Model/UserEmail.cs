using System.Collections.Generic;
using Core.Domain;
using Dawn;

namespace User.Domain.UserAggregate.Model
{
    public class UserEmail : ValueObject<UserEmail>
    {
        public string Email { get; }

        protected UserEmail() {}

        public UserEmail(string email)
        {
            Email = Guard.Argument(email, nameof(email)).NotNull().NotEmpty();
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Email;
        }
    }
}