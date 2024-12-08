
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance
{
    public class AppDbContext: DbContext
    {
        //DbSets
        public required DbSet<User> Users { get; set;}

        public AppDbContext (DbContextOptions<AppDbContext> options): base (options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}