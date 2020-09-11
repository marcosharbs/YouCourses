using Core.Domain;
using System;

namespace User.Domain.UserAggregate.Model
{
    public class User : AggregateRoot
    {
        public virtual UserName UserName { get; }
        public virtual UserEmail UserEmail { get; }
        public virtual UserPassword UserPassword { get; }
        public virtual UserPicture UserPicture { get; }

        protected User() {}

        private User(Guid id, 
                     UserName userName, 
                     UserEmail userEmail, 
                     UserPassword userPassword, 
                     UserPicture userPicture) : base(id)
        {
            UserName = userName;
            UserEmail = userEmail;
            UserPassword = userPassword;
            UserPicture = userPicture;
        }

        public static User Create(Guid id, string name, string email, string password, string picture)
        {
            return new User(id, 
                            new UserName(name), 
                            new UserEmail(email), 
                            new UserPassword(password), 
                            new UserPicture(picture));
        }

        public static User Create(string name, string email, string password, string picture)
        {
            return Create(Guid.NewGuid(), name, email, password, picture);
        }
    }
}