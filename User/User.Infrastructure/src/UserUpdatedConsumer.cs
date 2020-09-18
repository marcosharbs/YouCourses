using Core.Infrastructure;
using System;

namespace User.Infrastructure
{
    public class UserUpdatedConsumer : RabbitConsumer<UserTeste>
    {
        protected override void Process(UserTeste payload)
        {
            Console.WriteLine("------------------------");
            Console.WriteLine("usuario atualizado");
            Console.WriteLine(payload.Id);
            Console.WriteLine(payload.Name);
            Console.WriteLine(payload.Email);
            Console.WriteLine(payload.ImageUrl);
            Console.WriteLine("------------------------");
        }
    }

    public class UserCreatedConsumer : RabbitConsumer<UserTeste>
    {
        protected override void Process(UserTeste payload)
        {
            Console.WriteLine("------------------------");
            Console.WriteLine("usuario criado");
            Console.WriteLine(payload.Id);
            Console.WriteLine(payload.Name);
            Console.WriteLine(payload.Email);
            Console.WriteLine(payload.ImageUrl);
            Console.WriteLine("------------------------");
        }
    }

    public class UserConsumer : RabbitConsumer<UserTeste>
    {
        protected override void Process(UserTeste payload)
        {
            Console.WriteLine("------------------------");
            Console.WriteLine("usuario criado/atualizado");
            Console.WriteLine(payload.Id);
            Console.WriteLine(payload.Name);
            Console.WriteLine(payload.Email);
            Console.WriteLine(payload.ImageUrl);
            Console.WriteLine("------------------------");
        }
    }

    public class UserTeste
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }
    }
}