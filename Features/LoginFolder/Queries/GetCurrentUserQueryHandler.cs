
using Features.DTOs;
using MediatR;

namespace Features.LoginFolder.Queries
{
    public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, UserResponseDto>
    {
        public Task<UserResponseDto> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}