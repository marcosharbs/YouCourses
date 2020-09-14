using FluentNHibernate.Mapping;

namespace User.Data.Mapping
{
    public class UserMap : ClassMap<User.Domain.UserAggregate.Model.User>
    {
        public UserMap()
        {
            Id(user => user.Id).GeneratedBy.Assigned();

            Component(user => user.UserName, user => {
                user.Map(userName => userName.Name).Not.Nullable().Length(255);
            }).Not.LazyLoad();

            Component(user => user.UserEmail, user => {
                user.Map(userEmail => userEmail.Email).Not.Nullable().Length(255);
            }).Not.LazyLoad();

            Component(user => user.UserPassword, user => {
                user.Map(userPassword => userPassword.Password).Not.Nullable().Length(255);
            }).Not.LazyLoad();

            Component(user => user.UserPicture, user => {
                user.Map(userPicture => userPicture.ImageUrl);
            }).Not.LazyLoad();
        }
    }
}