using User.Domain;
using Core.Application;
using System;
using User.Domain.UserAggregate.Event;

namespace User.Application
{
    public class SaveUserUseCase : UseCase<User.Domain.UserAggregate.Model.User, UserUnitOfWork>
    {
        private readonly User.Domain.UserAggregate.Model.User _user;

        public SaveUserUseCase(User.Domain.UserAggregate.Model.User user,
                               UserUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _user = user;
        }

        protected override User.Domain.UserAggregate.Model.User Action()
        {
            var users = _unitOfWork.GetUserRepository();

            var user = users.getUserByEmail(_user.UserEmail.Email);

            if(user != null && user.Id != null)
                throw new InvalidOperationException();

            users.AddOrUpdate(_user);

            _unitOfWork.AddEvent(UserCreated.Create(_user.Id, 
                                                    _user.UserName.Name, 
                                                    _user.UserEmail.Email, 
                                                    _user.UserPicture.ImageUrl));
            
            return _user;
        }
    }
}