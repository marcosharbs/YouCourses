using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using User.Data;
using NHibernate;
using User.Domain;
using Core.Domain;
using User.Infrastructure;

namespace User.RestApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            DomainHandlers.Register(typeof(UserCreatedHandler));
            DomainHandlers.Register(typeof(UserUpdatedHandler));
            new UserConsumer().init("LIBRARY");
            new UserCreatedConsumer().init("LIBRARY");
            new UserUpdatedConsumer().init("LIBRARY");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ISessionFactory>(sp => SessionHelper.GetSessionFactory(Configuration.GetSection("Database")["ConnectionSrtring"]));
            services.AddScoped<UserUnitOfWork, NHibernateUnitOfWork>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
