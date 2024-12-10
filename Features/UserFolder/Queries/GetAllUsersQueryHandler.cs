using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Features.UserFolder.Queries
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<User>>
    {
        private readonly IRepository<User> _repository;

        public GetAllUsersQueryHandler(IRepository<User> repository){
            _repository = repository;
        }
        public async Task<IEnumerable<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _repository.GetAllAsync();
            return users.Select(u=> u).OrderByDescending(u=> u.Score);
        }
    }
}