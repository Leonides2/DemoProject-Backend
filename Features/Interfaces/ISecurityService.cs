

using Domain.Entities;

namespace Features.Interfaces
{
    public interface ISecurityService
    {
        string GenerateToken(User user);
        string PasswordHasher(User user,string Password);
        Task<string> ValidatePassword(string Email, string Password);
    }
}