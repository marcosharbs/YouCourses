using System.Collections.Generic;
using Core.Domain;
using Dawn;

namespace Library.Domain.AuthorAggregate.Model
{
    public class AuthorName : ValueObject<AuthorName>
    {
        public string Name { get; }

        protected AuthorName() {}

        public AuthorName(string name)
        {
            Name = Guard.Argument(name, nameof(name)).NotNull().NotEmpty();
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
        }
    }
}