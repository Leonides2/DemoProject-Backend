
using Domain.Entities;
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
    }
}