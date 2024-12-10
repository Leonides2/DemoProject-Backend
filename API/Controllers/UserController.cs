
using Domain.Entities;
using Features.UserFolder.Commands;
using Features.UserFolder.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMemoryCache _memory;
        public UserController (IMediator mediator, IMemoryCache memory) 
        {
            _mediator = mediator;
            _memory = memory;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            IEnumerable<User>? users;
            if(_memory.TryGetValue("Users",out users)){
                return users != null ? users : [];
            }else{
                var data = await _mediator.Send(new GetAllUsersQuery());
                _memory.Set("Users", data);
                return data;
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateUserCommand command)
        {
            _memory.Remove("Users");
            await _mediator.Send(command);

            return Ok( new {
                status = "Success"
            });
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteUserCommand {ID = id};
            _memory.Remove("Users");
            await _mediator.Send(command);

            return Ok( new {
                status = "Success"
            });
        }
    }
}