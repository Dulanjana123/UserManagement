using Microsoft.EntityFrameworkCore;
using UserManagemnt.Models.MasterData;

namespace UserManagemnt.Data
{
    public class PMSDbContext : DbContext
    {
        public PMSDbContext(DbContextOptions<PMSDbContext> options) : base(options)
        {
        }

        public DbSet<Person> Person { get; set; }
        public DbSet<Email> Email { get; set; }
        public DbSet<Phone> Phone { get; set; }
        public DbSet<Address> Address { get; set; }
    }
}
