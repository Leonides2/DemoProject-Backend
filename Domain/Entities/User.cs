

namespace Domain.Entities
{
    public class User
    {
        public int ID { get; set;}
        public required string Username {get; set;}

        public required double Score {get; set;}
        public DateTime CreatedAt = DateTime.Now;
        
    }
}