using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User
    {
        public int ID { get; set;}
        public required string Username {get; set;}
        public required string Password {get; set;}
        
    }
}