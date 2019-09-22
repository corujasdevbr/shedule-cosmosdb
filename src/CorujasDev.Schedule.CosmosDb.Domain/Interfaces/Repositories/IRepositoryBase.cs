using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CorujasDev.Schedule.CosmosDb.Domain.Interfaces.Repositories
{
    public interface IRepositoryBase<TEntity> : IDisposable where TEntity : class
    {
        void Add(TEntity obj);
        TEntity GetById(string id);
        IEnumerable<TEntity> GetAll();
        void Update(string id, TEntity obj);
        void Remove(string id);
    }
}
