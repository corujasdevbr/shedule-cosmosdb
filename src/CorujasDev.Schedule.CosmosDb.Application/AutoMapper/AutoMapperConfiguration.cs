using AutoMapper;
using CorujasDev.Schedule.CosmosDb.Application.AutoMapper.Profiles;

namespace CorujasDev.Schedule.CosmosDb.Application.AutoMapper
{
    public class AutoMapperConfiguration
    {
        public static MapperConfiguration ConfigureMappings()
        {
            var mapperConfiguration = new MapperConfiguration(cfg => {
                cfg.AddProfile(new EntityToViewModel());
            });
            return mapperConfiguration;
        }
    }
}
