
using MediatR;

namespace Features.UserFolder.Commands
{
    public class CreateUserCommand: IRequest<Unit>
    {
        public required string Username {get; set;}
        public required double Score {get; set;}

    }
}