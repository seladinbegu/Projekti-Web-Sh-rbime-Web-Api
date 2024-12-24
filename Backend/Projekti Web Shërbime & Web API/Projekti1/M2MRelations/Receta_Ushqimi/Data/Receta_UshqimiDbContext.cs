using Microsoft.EntityFrameworkCore;
using Projekti1.Models;
using Projekti1.Receta; // Ensure this namespace includes the necessary models


public class Receta_UshqimiDbContext : DbContext
{
    public Receta_UshqimiDbContext(DbContextOptions<Receta_UshqimiDbContext> options) : base(options) { }

    // DbSet for the junction table
    public DbSet<RecetaUshqimi> RecetaUshqimi { get; set; }

    // Override OnModelCreating to configure relationships
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Ensure the Id is auto-incremented
        modelBuilder.Entity<RecetaUshqimi>()
            .Property(du => du.Id)
            .ValueGeneratedOnAdd(); // This tells EF Core to auto-generate the Id on insert

        modelBuilder.Entity<RecetaUshqimi>()
            .HasKey(du => du.Id);  // Ensure the Id is the primary key

        // Configure the relationship between Receta and RecetaUshqimi
        modelBuilder.Entity<RecetaUshqimi>()
            .HasOne(du => du.Receta) // One Receta can have many RecetaUshqimi
            .WithMany()              // No navigation property on Receta
            .HasForeignKey(du => du.RecetaId)
            .OnDelete(DeleteBehavior.Cascade);  // On delete cascade for Receta

        // Configure the relationship between Ushqimi and RecetaUshqimi
        modelBuilder.Entity<RecetaUshqimi>()
            .HasOne(du => du.Ushqimi) // One Ushqimi can have many RecetaUshqimi
            .WithMany()                // No navigation property on Ushqimi
            .HasForeignKey(du => du.UshqimiId)
            .OnDelete(DeleteBehavior.Cascade); // On delete cascade for Ushqimi

        // Ignore Dieta and Ushqimi if they are not part of this DbContext
        modelBuilder.Ignore<Receta>();  // Ignore the Dieta class if not included in this context
        modelBuilder.Ignore<Ushqimi>();  // Ignore the Ushqimi class if not included in this context
    }

}
