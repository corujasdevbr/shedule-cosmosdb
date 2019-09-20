using CorujasDev.Schedule.CosmosDb.Domain.Interfaces.Repositories;
using CorujasDev.Schedule.CosmosDb.Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CorujasDev.Schedule.CosmosDb.Infra.Ioc
{
    public static class NativeInjectorConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryContact, ContactRepository>();
            services.AddScoped<IRepositoryTodoItem, TodoItemRepository>();
        }
    }
}
