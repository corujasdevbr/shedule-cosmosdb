using CorujasDev.Schedule.CosmosDb.Domain.Entities;

namespace CorujasDev.Schedule.CosmosDb.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository<UserEntity>
    {
        UserEntity GetByEmailPassword(string email, string password);

        UserEntity GetByEmail(string email);
    }
}
