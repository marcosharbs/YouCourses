using User.Domain;
using Core.Application;
using User.Domain.UserAggregate.Event;

namespace User.Application
{
    public class UpdateUserUseCase : UseCase<User.Domain.UserAggregate.Model.User, UserUnitOfWork>
    {
        private readonly User.Domain.UserAggregate.Model.User _user;

        public UpdateUserUseCase(User.Domain.UserAggregate.Model.User user,
                                 UserUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _user = user;
        }

        protected override User.Domain.UserAggregate.Model.User Action()
        {
            var users = _unitOfWork.GetUserRepository();
        
            var user = users.GetById(_user.Id);

            user.UpdateName(_user.UserName.Name);

            user.UpdatePicture(_user.UserPicture.ImageUrl);

            users.AddOrUpdate(user);

            _unitOfWork.AddEvent(UserUpdated.Create(user.Id, 
                                                    user.UserName.Name, 
                                                    user.UserEmail.Email, 
                                                    user.UserPicture.ImageUrl));

            return user;
        }
    }
}