using AutoMapper;
using CorujasDev.Schedule.CosmosDb.Application.Interfaces;
using CorujasDev.Schedule.CosmosDb.Application.ViewModel.Contact;
using CorujasDev.Schedule.CosmosDb.Application.ViewModel.User;
using CorujasDev.Schedule.CosmosDb.Domain.Entities;
using CorujasDev.Schedule.CosmosDb.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace CorujasDev.Schedule.CosmosDb.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly IContactRepository _contactRepository;
        private readonly IUserRepository _usertRepository;
        private readonly IMapper _mapper;

        public ContactsController(IContactRepository contactRepository, IUserRepository usertRepository, IMapper mapper, IContactService contactService)
        {
            _contactRepository = contactRepository;
            _contactService = contactService;
            _usertRepository = usertRepository;
            _mapper = mapper;
        }

        // GET: api/Todo
        [HttpGet]
        public ActionResult<IEnumerable<ContactViewModel>> GetContacts()
        {
            try
            {
                string userId = HttpContext.User.Claims.First(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

                var contacts = _contactService.GetAll(userId);

                if (contacts == null)
                    return NotFound();

                return Ok(contacts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Todo/5
        [HttpGet("{id}")]
        public ActionResult<ContactViewModel> GetContactById(string id)
        {
            try
            {
                string userId = HttpContext.User.Claims.First(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

                ContactViewModel contact = _contactService.GetById(userId, id);

                if (contact == null)
                {
                    return NotFound();
                }

                return Ok(contact);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Todo/5
        [HttpGet("name/{name}")]
        public ActionResult<IEnumerable<ContactViewModel>> GetContactByName(string name)
        {
            try
            {
                string userId = HttpContext.User.Claims.First(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

                UserViewModel user = _mapper.Map<UserViewModel>(_usertRepository.GetById(userId));

                var contacts = user.Contacts.Where(c => c.FirstName.ToLower().Contains(name));

                if (contacts == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<List<ContactViewModel>>(contacts));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public IActionResult PostContact(ContactViewModel contact)
        {
            try
            {
                string userId = HttpContext.User.Claims.First(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

                UserViewModel user = _mapper.Map<UserViewModel>(_usertRepository.GetById(userId));

                if (user.Contacts == null)
                    user.Contacts = new List<ContactViewModel>();

                user.Contacts.Add(_mapper.Map<ContactViewModel>(contact));

                _usertRepository.Update(userId, _mapper.Map<UserEntity>(user));

                return CreatedAtAction(nameof(GetContactById), new { contact.id }, contact);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult PutContact(string id, ContactViewModel contact)
        {
            try
            {
                string userId = HttpContext.User.Claims.First(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

                UserViewModel user = _mapper.Map<UserViewModel>(_usertRepository.GetById(userId));

                var returnContact = user.Contacts.FirstOrDefault(c => c.id == id);

                if (returnContact == null)
                {
                    return NotFound();
                }

                var index = user.Contacts.IndexOf(returnContact);

                if (index != -1)
                    user.Contacts[index] = _mapper.Map<ContactViewModel>(contact);

                _usertRepository.Update(user.id, _mapper.Map<UserEntity>(user));

                return CreatedAtAction(nameof(GetContactById), new { contact.id }, contact);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteContact(string id)
        {
            try
            {

                string userId = HttpContext.User.Claims.First(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

                UserViewModel user = _mapper.Map<UserViewModel>(_usertRepository.GetById(userId));

                var returnContact = user.Contacts.FirstOrDefault(c => c.id == id);

                if (returnContact == null)
                {
                    return NotFound();
                }

                var index = user.Contacts.IndexOf(returnContact);

                if (index != -1)
                    user.Contacts.RemoveAt(index);

                _usertRepository.Update(user.id, _mapper.Map<UserEntity>(user));

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}