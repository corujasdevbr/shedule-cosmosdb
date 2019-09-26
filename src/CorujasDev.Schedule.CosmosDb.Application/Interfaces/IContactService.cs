using CorujasDev.Schedule.CosmosDb.Application.ViewModel.Contact;
using System.Collections.Generic;

namespace CorujasDev.Schedule.CosmosDb.Application.Interfaces
{
    public interface IContactService
    {

        void Add(string userId, ContactViewModel obj);
        ContactViewModel GetById(string userId, string id);
        IEnumerable<ContactViewModel> GetAll(string userId);
        void Update(string id, ContactViewModel obj);
        void Remove(string id);

    }
}
