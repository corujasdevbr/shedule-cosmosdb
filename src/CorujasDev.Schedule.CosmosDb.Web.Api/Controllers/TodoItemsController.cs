using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using AutoMapper;
using CorujasDev.Schedule.CosmosDb.Application.ViewModel.TodoItem;
using CorujasDev.Schedule.CosmosDb.Application.ViewModel.User;
using CorujasDev.Schedule.CosmosDb.Domain.Entities;
using CorujasDev.Schedule.CosmosDb.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CorujasDev.Schedule.CosmosDb.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoItemRepository _TodoItemRepository;
        private readonly IUserRepository _usertRepository;
        private readonly IMapper _mapper;

        public TodoItemsController(ITodoItemRepository TodoItemRepository, IUserRepository usertRepository, IMapper mapper)
        {
            _TodoItemRepository = TodoItemRepository;
            _usertRepository = usertRepository;
            _mapper = mapper;
        }

        // GET: api/Todo
        [HttpGet]
        public ActionResult<IEnumerable<TodoItemViewModel>> GetTodoItems()
        {
            try
            {
                string userId = HttpContext.User.Claims.First(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

                UserViewModel user = _mapper.Map<UserViewModel>(_usertRepository.GetById(userId));

                return Ok(_mapper.Map<IEnumerable<TodoItemViewModel>>(user.TodoItems));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Todo/5
        [HttpGet("{id}")]
        public ActionResult<TodoItemViewModel> GetTodoItemById(string id)
        {
            try
            {
                string userId = HttpContext.User.Claims.First(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

                UserViewModel user = _mapper.Map<UserViewModel>(_usertRepository.GetById(userId));

                var TodoItem = user.TodoItems.FirstOrDefault(c => c.id == id);

                if (TodoItem == null)
                {
                    return NotFound();
                }

                return _mapper.Map<TodoItemViewModel>(TodoItem);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Todo/5
        [HttpGet("name/{name}")]
        public ActionResult<IEnumerable<TodoItemViewModel>> GetTodoItemByName(string name)
        {
            try
            {
                string userId = HttpContext.User.Claims.First(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

                UserViewModel user = _mapper.Map<UserViewModel>(_usertRepository.GetById(userId));

                var TodoItems = user.TodoItems.Where(c => c.Name.ToLower().Contains(name));

                if (TodoItems == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<IEnumerable<TodoItemViewModel>>(TodoItems));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="TodoItem"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public IActionResult PostTodoItem(TodoItemViewModel TodoItem)
        {
            try
            {
                string userId = HttpContext.User.Claims.First(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

                UserViewModel user = _mapper.Map<UserViewModel>(_usertRepository.GetById(userId));

                if (user.TodoItems == null)
                    user.TodoItems = new List<TodoItemViewModel>();

                user.TodoItems.Add(_mapper.Map<TodoItemViewModel>(TodoItem));

                _usertRepository.Update(userId, _mapper.Map<UserEntity>(user));

                return CreatedAtAction(nameof(GetTodoItemById), new { TodoItem.id }, TodoItem);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult PutTodoItem(string id, TodoItemViewModel TodoItem)
        {
            try
            {
                string userId = HttpContext.User.Claims.First(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

                UserViewModel user = _mapper.Map<UserViewModel>(_usertRepository.GetById(userId));

                var returnTodoItem = user.TodoItems.FirstOrDefault(c => c.id == id);

                if (returnTodoItem == null)
                {
                    return NotFound();
                }

                var index = user.TodoItems.IndexOf(returnTodoItem);

                if (index != -1)
                    user.TodoItems[index] = _mapper.Map<TodoItemViewModel>(TodoItem);

                _usertRepository.Update(user.id, _mapper.Map<UserEntity>(user));

                return CreatedAtAction(nameof(GetTodoItemById), new { TodoItem.id }, TodoItem);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteTodoItem(string id)
        {
            try
            {

                string userId = HttpContext.User.Claims.First(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

                UserViewModel user = _mapper.Map<UserViewModel>(_usertRepository.GetById(userId));

                var returnTodoItem = user.TodoItems.FirstOrDefault(c => c.id == id);

                if (returnTodoItem == null)
                {
                    return NotFound();
                }

                var index = user.TodoItems.IndexOf(returnTodoItem);

                if (index != -1)
                    user.TodoItems.RemoveAt(index);

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