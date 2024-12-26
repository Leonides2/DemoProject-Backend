

using Domain.Entities;

namespace Features.DTOs
{
    public class UserResponseDto
    {
        public int ID { get; set;}
        public required string Username {get; set;}
        public string? Email {get; set;}
        public required double Score {get; set;}
        public DateTime CreatedAt {get;}
        public DateTime UpdatedAt {set; get;}

        public static UserResponseDto FromUser( User user){
            return new UserResponseDto{
                ID = user.ID,
                Score = user.Score,
                Username = user.Username,
                Email = user.Email,
                UpdatedAt = user.UpdatedAt
            };
        }
        
    }
}