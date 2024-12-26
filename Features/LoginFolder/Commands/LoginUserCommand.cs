
using MediatR;

namespace Features.LoginFolder.Commands
{
    public class LoginUserCommand : IRequest<string>
    {
        public required string Username {get; set;}
        public required string Password {get; set;}
    }
}