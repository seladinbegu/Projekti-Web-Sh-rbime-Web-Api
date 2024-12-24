using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;

namespace Projekti1.User.Data
{
    public class UserDbContext : IdentityDbContext<User>
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }


        public DbSet<User> User { get; set; }



        public DbSet<RefreshToken> RefreshToken { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure the relationship between User and RefreshToken
            builder.Entity<RefreshToken>()
                .HasOne(r => r.User)  // Each RefreshToken belongs to one User
                .WithMany()  // A User can have many RefreshTokens
                .HasForeignKey(r => r.UserId)  // Foreign key property in RefreshToken
                .OnDelete(DeleteBehavior.Cascade);  // Optional: Set delete behavior if necessary
        }
    }
}