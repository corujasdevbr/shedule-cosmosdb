using CorujasDev.Schedule.CosmosDb.Domain.Entities;
using CorujasDev.Schedule.CosmosDb.Domain.Interfaces.Repositories;
using CorujasDev.Schedule.CosmosDb.Infra.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<ActionResult<IEnumerable<Contact>>> GetContacts()
        {
            try
            {
                var contacts = await _contactRepository.GetAll();

                return Ok(contacts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Todo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> GetContactById(Guid id)
        {
            try
            {
                var contact = await _contactRepository.GetById(id);

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

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostContact(Contact contact)
        {
            try
            {
                await _contactRepository.Add(contact);

                return CreatedAtAction(nameof(GetContactById), new { id = contact.ID }, contact);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}