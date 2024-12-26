
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
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci");

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.ID).HasName("users_pkey");

                entity.ToTable("users");

                entity.Property(e => e.ID).HasColumnName("id");
                entity.Property(e => e.Username)
                    .HasColumnType("varchar(36)")
                    .HasColumnName("username");
                entity.Property(e => e.Userkey)
                    .HasColumnType("varchar(36)")
                    .HasColumnName("userkey");
                entity.Property(e => e.Email)
                    .HasColumnType("varchar(100)")
                    .HasColumnName("email");
                entity.Property(e => e.Score)
                    .HasColumnType("decimal(16,2)")
                    .HasColumnName("score");
                entity.Property(e => e.Password)
                    .HasColumnType("text")
                    .HasColumnName("password");
                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("createdat");
                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updatedat");

            });

        }        
    }
}