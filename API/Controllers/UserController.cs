
using Domain.Entities;
using Features.UserFolder.Commands;
using Features.UserFolder.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController (IMediator mediator) 
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            return await _mediator.Send(new GetAllUsersQuery());
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateUserCommand command)
        {
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
            await _mediator.Send(command);

            return Ok( new {
                status = "Success"
            });
        }
    }
}