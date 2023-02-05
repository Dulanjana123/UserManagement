using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace UserManagemnt.Data
{
    /// <summary>
    /// Inserting EF Core authentication database with our data
    /// </summary>
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //define unique identifiers
            var superAdminRoleId = "462ae8c2-0faa-464b-a4d0-4cfe5d63cc80";
            var adminRoleId = "80e93644-e2ec-4c3c-b8f4-858eef691190";
            var userRoleId = "43f2add8-7679-4ae5-8b00-c68ea1ee24e4";

            // Seed Roles (User, Admin, Super Admin)
            // Define roles
            // Create roles List variable using IdentityRole model
            var roles = new List<IdentityRole>
            {
                //Super Admin role
                new IdentityRole()
                {
                    Name = "SuperAdmin",
                    NormalizedName = "SuperAdmin",
                    Id = superAdminRoleId,
                    ConcurrencyStamp = superAdminRoleId
                },

                //Admin role
                new IdentityRole()
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId
                },

                //User role
                new IdentityRole()
                {
                    Name = "User",
                    NormalizedName = "User",
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId
                }
            };

            //Seed above created roles into the database.
            //When EF core migration command execute above 3 roles will insert into the DB
            builder.Entity<IdentityRole>().HasData(roles);

            //Seed Super Admin User

            //Id of the super admin user
            var superAdminId = "7cbbb112-0a25-41fb-8143-5938cf30209e";

            //Create variable for superAdminUser using identity user object  
            //Identity user using Identity.EntityFrameworkCore
            var superAdminUser = new IdentityUser()
            {
                Id = superAdminId,
                UserName = "superadmin@bloggie.com",
                Email = "superadmin@bloggie.com",
                NormalizedEmail = "superadmin@bloggie.com".ToUpper(),
                NormalizedUserName = "superadmin@bloggie.com".ToUpper()
            };

            //generate password for the superAdminUser
            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>()
                                    .HashPassword(superAdminUser, "superadmin123");

            //When EF core migration command execute insert superAdminUser into the DB
            builder.Entity<IdentityUser>().HasData(superAdminUser);

            //Add All Roles To Super Admin User
            var superAdminRoles = new List<IdentityUserRole<string>>()
            {
                new IdentityUserRole<string>
                {
                    RoleId = superAdminRoleId,
                    UserId = superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = userRoleId,
                    UserId = superAdminId
                },
            };

            //When EF core migration command execute insert superAdminRoles into the DB
            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);

        }
    }
}
