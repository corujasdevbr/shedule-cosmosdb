﻿using CorujasDev.Schedule.CosmosDb.Domain.Entities;
using CorujasDev.Schedule.CosmosDb.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace CorujasDev.Schedule.CosmosDb.Infra.Data.Repositories
{
    public class ContactRepository : RepositoryBaseDocumentClient<ContactEntity>, IContactRepository
    {
        public ContactRepository()
        {

        }

        public IEnumerable<ContactEntity> GetByName(string name)
        {
            return base.GetAll().Where(c => c.FirstName.Contains(name));
        }
    }
}
