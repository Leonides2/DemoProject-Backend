

using Features.DTOs;
using Features.LoginFolder.Commands;
using Features.UserFolder.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LoginController (IMediator mediator) 
        {
            _mediator = mediator;

        }


        [HttpPost]
        public async Task<IActionResult> Post(LoginUserCommand command)
        {

            var data = await _mediator.Send(command);

            return Ok( 
                new {response = data}
            );
        }
        
    }
}