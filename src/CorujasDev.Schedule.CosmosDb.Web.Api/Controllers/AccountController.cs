using AutoMapper;
using CorujasDev.Schedule.CosmosDb.Application.ViewModel.Account;
using CorujasDev.Schedule.CosmosDb.Application.ViewModel.User;
using CorujasDev.Schedule.CosmosDb.Domain.Entities;
using CorujasDev.Schedule.CosmosDb.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CorujasDev.Schedule.CosmosDb.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AccountController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="login"></param>
        /// <returns>Jwt</returns>
        [HttpPost("login")]
        public IActionResult PostLogin(LoginViewModel login)
        {
            try
            {
                UserViewModel user = _mapper.Map<UserViewModel>(_userRepository.GetByEmailPassword(login.Email, login.Password));

                if (user == null)
                    return NotFound();

                //Define os dados que serão fornecidos no token - PayLoad
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.Name),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, user.id.ToString())
                };

                // Chave de acesso do token
                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("corujasdev-schedule-key-auth"));

                //Credenciais do Token - Header
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                //Gera o token
                var token = new JwtSecurityToken(
                    issuer: "CorujasDev.Schedule",
                    audience: "CorujasDev.Schedule",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds
                );

                //Retorna Ok com o Token
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="login"></param>
        /// <returns>Jwt</returns>
        [HttpPost("create")]
        public IActionResult CreateUser(CreateUserViewModel userCreate)
        {
            try
            {
                
                UserViewModel user = _mapper.Map<UserViewModel>(_userRepository.GetByEmail(userCreate.Email));

                if (user != null)
                    return BadRequest(new { mensagem = "E-mail already registered" });

                _userRepository.Add(new UserEntity(userCreate.Name, userCreate.Email, userCreate.Password));

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}