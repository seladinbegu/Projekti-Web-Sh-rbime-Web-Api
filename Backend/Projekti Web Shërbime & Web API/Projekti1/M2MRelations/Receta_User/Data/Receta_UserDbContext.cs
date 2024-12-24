using Microsoft.EntityFrameworkCore;
using Projekti1.Receta;
using Projekti1.User;

public class Receta_UserDbContext : DbContext
{
    public DbSet<RecetaUser> RecetaUser { get; set; }

    public Receta_UserDbContext(DbContextOptions<Receta_UserDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Ensure the Id is auto-incremented
        modelBuilder.Entity<RecetaUser>()
            .Property(du => du.Id)
            .ValueGeneratedOnAdd(); // Auto-generate the Id on insert

        modelBuilder.Entity<RecetaUser>()
            .HasKey(du => du.Id);  // Ensure the Id is the primary key

        // Configure the relationship between Receta and RecetaUser
        modelBuilder.Entity<RecetaUser>()
            .HasOne(du => du.Receta) // One Receta can have many RecetaUser
            .WithMany()  // No navigation property in Receta for RecetaUser
            .HasForeignKey(du => du.RecetaId)
            .OnDelete(DeleteBehavior.Cascade);  // On delete cascade for Receta

        // Configure the relationship between User and RecetaUser
        modelBuilder.Entity<RecetaUser>()
            .HasOne(du => du.User) // One User can have many RecetaUser
            .WithMany()  // No navigation property in User for RecetaUser
            .HasForeignKey(du => du.UserId)
            .OnDelete(DeleteBehavior.Cascade); // On delete cascade for User

        modelBuilder.Ignore<Receta>();  // Ignore the Dieta class if not included in this context
        modelBuilder.Ignore<User>();
    }
}
