using User.Domain;
using Core.Application;

namespace User.Application
{
    public class UpdateUserUseCase : UseCase<User.Domain.UserAggregate.Model.User, IUserUnitOfWork>
    {
        private readonly User.Domain.UserAggregate.Model.User _user;

        public UpdateUserUseCase(User.Domain.UserAggregate.Model.User user,
                                 IUserUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _user = user;
        }

        protected override User.Domain.UserAggregate.Model.User Action()
        {
            var users = _unitOfWork.Users;
        
            var user = users.GetById(_user.Id);

            user.UpdateName(_user.UserName.Name);

            user.UpdatePicture(_user.UserPicture.ImageUrl);

            users.AddOrUpdate(user);

            return user;
        }
    }
}