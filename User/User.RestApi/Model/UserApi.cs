using System;

namespace User.RestApi.Model
{
    public class UserApi
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ImageUrl { get; set; }

        public UserApi() { }

        public UserApi(Guid id, string name, string email, string password, string imageUrl) {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
            ImageUrl = imageUrl;
        }

        public static UserApi FromDomain(User.Domain.UserAggregate.Model.User user)
        {
            return new UserApi(user.Id, 
                               user.UserName.Name,
                               user.UserEmail.Email, 
                               user.UserPassword.Password, 
                               user.UserPicture.ImageUrl);
        }

        public static User.Domain.UserAggregate.Model.User ToDomain(UserApi user)
        {
            return User.Domain.UserAggregate.Model.User.Create(user.Id, 
                                                               user.Name, 
                                                               user.Email, 
                                                               user.Password, 
                                                               user.ImageUrl);
        }
    }
}