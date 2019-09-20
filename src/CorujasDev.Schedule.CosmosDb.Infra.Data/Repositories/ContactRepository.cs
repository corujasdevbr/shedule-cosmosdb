using CorujasDev.Schedule.CosmosDb.Domain.Entities;
using CorujasDev.Schedule.CosmosDb.Domain.Interfaces.Repositories;
using CorujasDev.Schedule.CosmosDb.Infra.Data.Contexts;

namespace CorujasDev.Schedule.CosmosDb.Infra.Data.Repositories
{
    public class ContactRepository : RepositoryBase<Contact>, IRepositoryContact
    {
        public ContactRepository(ScheduleContext context) : base(context) { }
    }
}
