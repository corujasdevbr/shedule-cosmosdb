using CorujasDev.Schedule.CosmosDb.Domain.Entities;
using System.Collections.Generic;

namespace CorujasDev.Schedule.CosmosDb.Domain.Interfaces.Repositories
{
    public interface IRepositoryContact : IRepositoryBase<Contact>
    {
        IEnumerable<Contact> GetByName(string name);
    }
}
