

using Domain.Entities;
using Features.DTOs;
using MediatR;

namespace Features.LoginFolder.Queries
{
    public class GetCurrentUserQuery : IRequest<UserResponseDto>
    {
        public required string Token {get; set;}
    }
}