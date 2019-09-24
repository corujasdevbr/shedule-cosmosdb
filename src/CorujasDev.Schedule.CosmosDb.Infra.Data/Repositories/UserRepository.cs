using CorujasDev.Schedule.CosmosDb.Domain.Entities;
using CorujasDev.Schedule.CosmosDb.Domain.Interfaces.Repositories;
using System;
using System.Linq;

namespace CorujasDev.Schedule.CosmosDb.Infra.Data.Repositories
{
    public class UserRepository : RepositoryBaseDocumentClient<UserEntity>, IUserRepository
    {
        public UserEntity GetByEmail(string email)
        {
            try
            {
                return base.GetAll().FirstOrDefault(c => c.Email.ToLower() == email.ToLower());
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public UserEntity GetByEmailPassword(string email, string password)
        {
            try
            {
                return base.GetAll().FirstOrDefault(c => c.Email.ToLower() == email.ToLower() && c.Password == password);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
