using Microsoft.EntityFrameworkCore;
using Projekti1.Models;

namespace Projekti1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions)
           : base(dbContextOptions)
        { }


        public DbSet<Receta> Receta { get; set; }
    }
}
