using AutoMapper;
using CorujasDev.Schedule.CosmosDb.Application.Interfaces;
using CorujasDev.Schedule.CosmosDb.Application.ViewModel.Contact;
using CorujasDev.Schedule.CosmosDb.Application.ViewModel.User;
using CorujasDev.Schedule.CosmosDb.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CorujasDev.Schedule.CosmosDb.Application.Services
{
    public class ContactService : IContactService
    {
        private readonly IUserRepository _usertRepository;
        private readonly IMapper _mapper;

        public ContactService(IMapper mapper, IUserRepository usertRepository)
        {
            _mapper = mapper;
            _usertRepository = usertRepository;
        }

        public void Add(string userId, ContactViewModel obj)
        {
            
        }

        public IEnumerable<ContactViewModel> GetAll(string userId)
        {
            try
            {
                UserViewModel user = _mapper.Map<UserViewModel>(_usertRepository.GetById(userId));

                return _mapper.Map<List<ContactViewModel>>(user.Contacts);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public ContactViewModel GetById(string userId, string id)
        {
            UserViewModel user = _mapper.Map<UserViewModel>(_usertRepository.GetById(userId));

            var contact = user.Contacts.FirstOrDefault(c => c.id == id);

            if (contact == null)
            {
                return null;
            }

            return _mapper.Map<ContactViewModel>(contact);
        }

        public void Remove(string id)
        {
            throw new NotImplementedException();
        }

        public void Update(string id, ContactViewModel obj)
        {
            throw new NotImplementedException();
        }
    }
}
