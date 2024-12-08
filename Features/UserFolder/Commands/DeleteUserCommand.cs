
using MediatR;

namespace Features.UserFolder.Commands
{
    public class DeleteUserCommand : IRequest<Unit>
    {
        public required int ID {get; set;}
    }
}