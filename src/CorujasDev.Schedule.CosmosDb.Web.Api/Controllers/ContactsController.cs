using CorujasDev.Schedule.CosmosDb.Domain.Entities;
using CorujasDev.Schedule.CosmosDb.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CorujasDev.Schedule.CosmosDb.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IRepositoryContact _contactRepository;

        public ContactsController(IRepositoryContact contactRepository)
        {
            _contactRepository = contactRepository;
        }

        // GET: api/Todo
        [HttpGet]
        public ActionResult<IEnumerable<Contact>> GetContacts()
        {
            try
            {
                var contacts = _contactRepository.GetAll();

                return Ok(contacts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Todo/5
        [HttpGet("{id}")]
        public ActionResult<Contact> GetContactById(string id)
        {
            try
            {
                var contact = _contactRepository.GetById(id);

                if (contact == null)
                {
                    return NotFound();
                }

                return contact;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Todo/5
        [HttpGet("name/{name}")]
        public ActionResult<IEnumerable<Contact>> GetContactByName(string name)
        {
            try
            {
                var contacts = _contactRepository.GetByName(name);

                if (contacts == null)
                {
                    return NotFound();
                }

                return Ok(contacts);
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
        public IActionResult PostContact(Contact contact)
        {
            try
            {
                _contactRepository.Add(contact);

                return CreatedAtAction(nameof(GetContactById), new { contact.id }, contact);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult PutContact(string id, Contact contact)
        {
            try
            {

                var returnContact = _contactRepository.GetById(id);

                if (returnContact == null)
                {
                    return NotFound();
                }

                _contactRepository.Update(id, contact);

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

                var returnContact = _contactRepository.GetById(id);

                if (returnContact == null)
                {
                    return NotFound();
                }

                _contactRepository.Remove(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}