
using Microsoft.AspNetCore.Identity;

using Features.Interfaces;
using Infrastructure.Persistance;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Shared.Settings;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace Infrastructure.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly JwtSettings _jwtSettings;

        public SecurityService(AppDbContext context, IPasswordHasher<User> passwordHasher, IOptions<JwtSettings> options){
            _context = context;
            _passwordHasher = passwordHasher;
            _jwtSettings = options.Value;
        }
        public string GenerateToken(User user)
        {
            if (user.Email != null)
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key ?? "thisadefaultsecret"));
			    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

			    var claims = new List<Claim>
			    {
				    new Claim(ClaimTypes.Email, user.Email),
			    };

			    var token = new JwtSecurityToken(
				    claims: claims,
				    notBefore: DateTime.UtcNow,
				    expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpireMinutes),
				    signingCredentials: creds);

			    return new JwtSecurityTokenHandler().WriteToken(token);

            }
            else{
                return string.Empty;
            }
            
        }

        public string PasswordHasher( User user,string Password)
        {
            return _passwordHasher.HashPassword(user ,Password);
        }

        public async Task<string> ValidatePassword(string Email, string Password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == Email);
            if(user != null && user.Password != null){
                var result = _passwordHasher.VerifyHashedPassword(user, user.Password, Password);

                if(result == PasswordVerificationResult.Success){
                    return GenerateToken(user);
                }
                return string.Empty;
            }else{
                return string.Empty;
            }
        }
    }
}