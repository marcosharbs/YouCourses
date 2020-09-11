using System.Collections.Generic;
using Core.Domain;
using Dawn;

namespace User.Domain.UserAggregate.Model
{
    public class UserName : ValueObject<UserName>
    {
        public string Name { get; }

        protected UserName() {}

        public UserName(string name)
        {
            Name = Guard.Argument(name, nameof(name)).NotNull().NotEmpty();
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
        }
    }
}