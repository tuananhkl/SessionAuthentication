using AuthenAuthor.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthenAuthor.DbContext;

public class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<User>().Property(x => x.DateOfBirth).HasColumnType("datetime");

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                UserName = "tuananh",
                Password = "Tuananh123.",
                DateOfBirth = new DateTime(1999, 08, 30),
                Address = "Hanoi",
                Email = "tuananh@gmail.com",
                Age = 23,
                Gender = true
            });
    }
}