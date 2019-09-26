using CorujasDev.Schedule.CosmosDb.Application.Interfaces;
using CorujasDev.Schedule.CosmosDb.Application.Services;
using CorujasDev.Schedule.CosmosDb.Domain.Interfaces.Repositories;
using CorujasDev.Schedule.CosmosDb.Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CorujasDev.Schedule.CosmosDb.Infra.Ioc
{
    public static class NativeInjectorConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<ITodoItemRepository, TodoItemRepository>();
            services.AddScoped<IUserRepository, UserRepository>();


            services.AddScoped<IContactService, ContactService>();
        }
    }
}
