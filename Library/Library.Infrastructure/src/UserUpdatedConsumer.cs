using Core.Infrastructure;
using Library.Data;
using Library.Application;

namespace Library.Infrastructure
{
    public class UserUpdatedConsumer : RabbitConsumer<UserBroker>
    {
        protected override void Process(UserBroker payload)
        {
            var databaseConnection = "Server=127.0.0.1;Port=5432;User ID=postgres;Password=admin;Database=YouCourse.Library";
            var sessionFactory = SessionHelper.GetSessionFactory(databaseConnection);
            var unitOfWork = new NHibernateUnitOfWork(sessionFactory);
            var useCase = new UpdateAuthorUseCase(payload.ToDomain(), unitOfWork);
            useCase.Execute();
        }
    }
}