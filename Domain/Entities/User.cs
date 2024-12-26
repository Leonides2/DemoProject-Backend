

namespace Domain.Entities
{
    public class User
    {
        public int ID { get; set;}
        public required string Username {get; set;}
        public string? Password {get; set;}
        public required string Userkey {get; set;}
        public string? Email {get; set;}
        public required double Score {get; set;}
        public DateTime CreatedAt {get;}
        public DateTime UpdatedAt {set; get;}

       
    }
}