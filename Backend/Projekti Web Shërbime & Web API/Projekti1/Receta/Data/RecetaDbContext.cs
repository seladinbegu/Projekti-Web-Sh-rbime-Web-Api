using Microsoft.EntityFrameworkCore;
using Projekti1.Models;

namespace Projekti1.Data
{
    public class RecetaDbContext : DbContext
    {
        public RecetaDbContext(DbContextOptions<RecetaDbContext> options) : base(options) { }



        // public DbSet<Ushqimi> Receta { get; set; }
    }
}
