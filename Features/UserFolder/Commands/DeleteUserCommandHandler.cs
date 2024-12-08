using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;
using Features.HubFolder;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Features.UserFolder.Commands
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
    {
        private readonly IRepository<User> _repository;
        private readonly IHubContext<UserHub> _hub;
        public DeleteUserCommandHandler(IRepository<User> repository, IHubContext<UserHub> hub){
            _repository = repository;
            _hub = hub;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var User = await _repository.GetByIdAsync(request.ID);
            await _repository.DeleteAsync( request.ID);

            await _hub.Clients.All.SendAsync("User Deleted", User.Username, User.Score);

            return Unit.Value;
        }
    }
}