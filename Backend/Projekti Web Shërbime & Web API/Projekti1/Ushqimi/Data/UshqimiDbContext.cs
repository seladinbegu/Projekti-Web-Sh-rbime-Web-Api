using Microsoft.EntityFrameworkCore;
using Projekti1.Models;

namespace Projekti1.Data
{
    public class UshqimiDbContext : DbContext
    {
        public UshqimiDbContext(DbContextOptions<UshqimiDbContext> options) : base(options) { }



        public DbSet<Ushqimi> Ushqimi { get; set; }
    }
}
