using Microsoft.EntityFrameworkCore;
using Users.Core;

namespace Users.Infrastucture
{
    public class UsersDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "UsersDb");
        }

        public DbSet<User> Users { get; set; }
    }
}