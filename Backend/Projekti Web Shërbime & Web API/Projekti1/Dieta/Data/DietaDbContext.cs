using Microsoft.EntityFrameworkCore;
using Projekti1.Models;

namespace Projekti1.Data
{
    public class DietaDbContext : DbContext
    {
        public DietaDbContext(DbContextOptions<DietaDbContext> options) : base(options) { }



        // public DbSet<Ushqimi> Dieta { get; set; }
    }
}
