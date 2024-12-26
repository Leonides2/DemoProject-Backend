
using Features.DTOs;
using MediatR;

namespace Features.UserFolder.Queries
{
    public class GetAllUsersQuery: IRequest<IEnumerable<UserResponseDto>>
    {
        public string? Username {get; set;}
        public int? page {get; set;}
        public int? limit {get; set;}
    }
}