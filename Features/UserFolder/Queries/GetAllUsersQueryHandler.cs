
using Domain.Entities;
using Domain.Repositories;
using Features.DTOs;
using MediatR;

namespace Features.UserFolder.Queries
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserResponseDto>>
    {
        private readonly IRepository<User> _repository;

        public GetAllUsersQueryHandler(IRepository<User> repository){
            _repository = repository;
        }
        public async Task<IEnumerable<UserResponseDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _repository.GetAllAsync();
            return users.Select(UserResponseDto.FromUser).OrderByDescending(u=> u.Score);
        }
    }
}