using CorujasDev.Schedule.CosmosDb.Domain.Entities;
using System.Collections.Generic;

namespace CorujasDev.Schedule.CosmosDb.Domain.Interfaces.Repositories
{
    public interface IContactRepository : IBaseRepository<ContactEntity>
    {
        IEnumerable<ContactEntity> GetByName(string name);
    }
}
