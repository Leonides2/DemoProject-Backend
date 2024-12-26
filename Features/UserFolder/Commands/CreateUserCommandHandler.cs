
using Domain.Entities;
using Domain.Repositories;
using Features.HubFolder;
using Features.Interfaces;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Features.UserFolder.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Unit>
    {
        private readonly IRepository<User> _repository;
        private readonly IHubContext<UserHub> _hub;
        private readonly ISecurityService _securityService;

        public CreateUserCommandHandler(IRepository<User> repository, IHubContext<UserHub> hub, ISecurityService securityService){
            _hub = hub;
            _repository = repository;
            _securityService = securityService;
        }

        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var User = new User{
                Username = request.Username,
                Score = request.Score,
                Userkey = request.Username.Trim().ToLower(),
                Email = string.IsNullOrEmpty(request.Email) ? null : request.Email.Trim().ToLower()
            };

            if(!string.IsNullOrEmpty(request.Password))  User.Password = _securityService.PasswordHasher( User, request.Password.Trim());

            await _repository.AddAsync(User);

            await _hub.Clients.All.SendAsync("User Created", User.Username, request.Score);
            return Unit.Value;
        }
    }
}