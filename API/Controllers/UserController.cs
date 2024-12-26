

using Features.DTOs;
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
        private readonly ILogger<UserController> _logger;
        public UserController(IMediator mediator, IMemoryCache memory, ILogger<UserController> logger)
        {
            _mediator = mediator;
            _memory = memory;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<UserResponseDto>> Get()
        {
            if (!_memory.TryGetValue("Users", out IEnumerable<UserResponseDto>? users))
            {
                var data = await _mediator.Send(new GetAllUsersQuery());
                if (data?.Any() == true)
                {
                    _logger.LogInformation("Creating cache...");
                    _memory.Set("Users", data);
                }
                return data ?? Enumerable.Empty<UserResponseDto>();
            }
            return users ?? Enumerable.Empty<UserResponseDto>();

        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateUserCommand command)
        {

            if (_memory.TryGetValue("Users", out _))
            {
                _logger.LogInformation("Cache exists. Removing...");
                _memory.Remove("Users");
            }
            else
            {
                _logger.LogInformation("Cache does not exist. No removal needed.");
            }
            await _mediator.Send(command);

            return Ok(new
            {
                status = "Success"
            });
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            _memory.Remove("Users");
            await _mediator.Send(new DeleteUserCommand { ID = id });

            return Ok(new
            {
                status = "Success"
            });
        }
    }
}