
using Domain.Entities;
using Domain.Repositories;
using Features.Interfaces;
using MediatR;

namespace Features.LoginFolder.Commands
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, string>
    {
        private readonly ISecurityService _securityService;

        public LoginUserCommandHandler (ISecurityService securityService){
            _securityService = securityService;

        }
        public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _securityService.ValidatePassword(request.Username,request.Password);

            if(result == null){
                return string.Empty;
            }

            return result;

        }
    }
}