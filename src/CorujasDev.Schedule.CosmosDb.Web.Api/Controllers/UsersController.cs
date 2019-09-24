using AutoMapper;
using CorujasDev.Schedule.CosmosDb.Application.ViewModel.User;
using CorujasDev.Schedule.CosmosDb.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace CorujasDev.Schedule.CosmosDb.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _usertRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository usertRepository, IMapper mapper)
        {
            _usertRepository = usertRepository;
            _mapper = mapper;
        }

        // GET: api/users
        [HttpGet]
        public ActionResult<UserViewModel> GetUser()
        {
            try
            {
                string userId = HttpContext.User.Claims.First(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

                UserViewModel user = _mapper.Map<UserViewModel>(_usertRepository.GetById(userId));

                if (user == null)
                    return NotFound();

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}