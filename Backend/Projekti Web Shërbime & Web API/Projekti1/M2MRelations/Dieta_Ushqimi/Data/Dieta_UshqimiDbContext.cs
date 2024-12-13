using Microsoft.EntityFrameworkCore;
using Projekti1.Models; // Ensure this namespace includes Dieta, Ushqimi, and DietaUshqimi models

namespace Projekti1.M2MRelations.Data
{
    public class Dieta_UshqimiDbContext : DbContext
    {
        public Dieta_UshqimiDbContext(DbContextOptions<Dieta_UshqimiDbContext> options) : base(options) { }

        // DbSet for the junction table
        public DbSet<DietaUshqimi> DietaUshqimi { get; set; }

        // Override OnModelCreating to configure relationships
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Ensure the Id is auto-incremented
            modelBuilder.Entity<DietaUshqimi>()
                .Property(du => du.Id)
                .ValueGeneratedOnAdd(); // This tells EF Core to auto-generate the Id on insert

            modelBuilder.Entity<DietaUshqimi>()
                .HasKey(du => du.Id);

            // Configure relationships and delete behavior
            modelBuilder.Entity<DietaUshqimi>()
                .HasOne(du => du.Dieta)
                .WithMany()
                .HasForeignKey(du => du.DietaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DietaUshqimi>()
                .HasOne(du => du.Ushqimi)
                .WithMany()
                .HasForeignKey(du => du.UshqimiId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Ignore<Dieta>();
            modelBuilder.Ignore<Ushqimi>();
        }

    }
}
