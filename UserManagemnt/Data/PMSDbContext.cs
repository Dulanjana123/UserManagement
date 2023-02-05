using Microsoft.EntityFrameworkCore;
using UserManagemnt.Models.MasterData;

namespace UserManagemnt.Data
{
    //Bridge between Models classes and
    //the database
    public class PMSDbContext : DbContext
    {
        public PMSDbContext(DbContextOptions<PMSDbContext> options) : base(options)
        {
        }

        //Person property
        //Map Person property to sql server Person table
        public DbSet<Person> Person { get; set; }
        public DbSet<Email> Email { get; set; }
        public DbSet<Phone> Phone { get; set; }
        public DbSet<Address> Address { get; set; }
    }
}
