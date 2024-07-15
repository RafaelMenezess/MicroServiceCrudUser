using Microsoft.EntityFrameworkCore;

namespace MicroServiceCrudUser.Models.Context;

public class MySQLContext : DbContext
{
    public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
}
