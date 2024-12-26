
using MediatR;

namespace Features.UserFolder.Commands
{
    public class ChangeScoreCommand : IRequest<string>
    {
        public double NewScore {get; set;}

        public required string Token {get; set;}
    }
}