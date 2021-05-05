using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using STARK_Project.DatabaseModel;

namespace STARK_Project.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<Cryptocurrency> Cryptocurrenies { get; set; }
        public DbSet<Condition> Conditions { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().HasMany<Cryptocurrency>();
            builder.Entity<Cryptocurrency>().HasMany<User>();

            builder.Entity<Cryptocurrency>().HasMany<Condition>(x=>x.Conditions).WithOne(x=>x.Cryptocurrency);
            builder.Entity<User>().HasMany<Condition>(x=>x.Conditions).WithOne(x=>x.User);
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
