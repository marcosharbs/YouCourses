using Microsoft.AspNetCore.Mvc;
using User.Application;
using User.Domain;
using User.RestApi.Model;

namespace User.RestApi.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly UserUnitOfWork _unitOfWork;

        public UserController(UserUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public UserApi Post(UserApi userApi)
        {
            var user = UserApi.ToDomain(userApi);
            var newUser = new SaveUserUseCase(user, _unitOfWork).Execute();
            return UserApi.FromDomain(newUser);
        }

        [HttpPut]
        public UserApi Put(UserApi userApi)
        {
            var user = UserApi.ToDomain(userApi);
            var updatedUser = new UpdateUserUseCase(user, _unitOfWork).Execute();
            return UserApi.FromDomain(updatedUser);
        }
    }
}
