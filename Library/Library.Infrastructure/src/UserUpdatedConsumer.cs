using Core.Infrastructure;
using Library.Data;
using Library.Application;

namespace Library.Infrastructure
{
    public class UserUpdatedConsumer : RabbitConsumer<UserBroker>
    {
        protected override void Process(UserBroker payload)
        {
            //TODO: get database url from application configs.
            var databaseUrl = "Server=127.0.0.1;Port=5432;User ID=postgres;Password=admin;Database=YouCourse.Library";
            var useCase = new UpdateAuthorUseCase(payload.ToDomain(), NHibernateUnitOfWork.Create(databaseUrl));
            useCase.Execute();
        }
    }
}