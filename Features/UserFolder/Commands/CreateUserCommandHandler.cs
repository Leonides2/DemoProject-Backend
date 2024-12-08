
using Domain.Entities;
using Domain.Repositories;
using Features.HubFolder;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Features.UserFolder.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Unit>
    {
        private readonly IRepository<User> _repository;
        private readonly IHubContext<UserHub> _hub;

        public CreateUserCommandHandler(IRepository<User> repository,IHubContext<UserHub> hub){
            _hub = hub;
            _repository = repository;
        }

        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var User = new User{
                Username = request.Username,
                Score = request.Score
            };

            await _repository.AddAsync(User);

            await _hub.Clients.All.SendAsync("User Created", User.Username, request.Score);
            return Unit.Value;
        }
    }
}