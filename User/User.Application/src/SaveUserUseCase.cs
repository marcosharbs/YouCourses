using User.Domain;
using Core.Application;
using System;

namespace User.Application
{
    public class SaveUserUseCase : UseCase<User.Domain.UserAggregate.Model.User, IUserUnitOfWork>
    {
        private readonly User.Domain.UserAggregate.Model.User _user;

        public SaveUserUseCase(User.Domain.UserAggregate.Model.User user,
                               IUserUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _user = user;
        }

        protected override User.Domain.UserAggregate.Model.User Action()
        {
            var users = _unitOfWork.Users;

            var user = users.getUserByEmail(_user.UserEmail.Email);

            if(user != null && user.Id != null)
                throw new InvalidOperationException();

            users.AddOrUpdate(_user);
            
            return _user;
        }
    }
}