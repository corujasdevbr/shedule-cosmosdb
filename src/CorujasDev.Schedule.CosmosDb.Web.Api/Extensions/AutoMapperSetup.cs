using AutoMapper;
using CorujasDev.Schedule.CosmosDb.Application.AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CorujasDev.Schedule.CosmosDb.Web.Api.Extensions
{
    public static class AutoMapperSetup
    {
        public static void AddAutoMapperSetup(this IServiceCollection services)
        {
            var mapper = AutoMapperConfiguration.ConfigureMappings();
            services.AddAutoMapper(x => mapper.CreateMapper(), Assembly.Load("CorujasDev.Schedule.CosmosDb.Application"));
        }
    }
}
