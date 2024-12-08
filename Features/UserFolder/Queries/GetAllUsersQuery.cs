using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using MediatR;

namespace Features.UserFolder.Queries
{
    public class GetAllUsersQuery: IRequest<IEnumerable<User>>
    {
        public string? Username {get; set;}
        public int? page {get; set;}
        public int? limit {get; set;}
    }
}