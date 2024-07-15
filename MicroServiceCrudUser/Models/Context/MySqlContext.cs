using Microsoft.EntityFrameworkCore;

namespace MicroServiceCrudUser.Models.Context;

public class MySQLContext : DbContext
{
    public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Username = "johndoe",
                PasswordHash = "hash1",
                Email = "john.doe@example.com"
            },
            new User
            {
                Id = 2,
                Username = "janesmith",
                PasswordHash = "hash2",
                Email = "jane.smith@example.com"
            },
            new User
            {
                Id = 3,
                Username = "michaelbrown",
                PasswordHash = "hash3",
                Email = "michael.brown@example.com"
            },
            new User
            {
                Id = 4,
                Username = "lisajones",
                PasswordHash = "hash4",
                Email = "lisa.jones@example.com"
            },
            new User
            {
                Id = 5,
                Username = "chrismiller",
                PasswordHash = "hash5",
                Email = "chris.miller@example.com"
            },
            new User
            {
                Id = 6,
                Username = "patriciawilson",
                PasswordHash = "hash6",
                Email = "patricia.wilson@example.com"
            },
            new User
            {
                Id = 7,
                Username = "roberttaylor",
                PasswordHash = "hash7",
                Email = "robert.taylor@example.com"
            },
            new User
            {
                Id = 8,
                Username = "lindamoore",
                PasswordHash = "hash8",
                Email = "linda.moore@example.com"
            },
            new User
            {
                Id = 9,
                Username = "williamthomas",
                PasswordHash = "hash9",
                Email = "william.thomas@example.com"
            },
            new User
            {
                Id = 10,
                Username = "barbaraclark",
                PasswordHash = "hash10",
                Email = "barbara.clark@example.com"
            }
        );
    }
}
