

using MediatR;

namespace Features.UserFolder.Commands
{
    public class ChangeScoreCommandHandler : IRequestHandler<ChangeScoreCommand, string>
    {
        public Task<string> Handle(ChangeScoreCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}