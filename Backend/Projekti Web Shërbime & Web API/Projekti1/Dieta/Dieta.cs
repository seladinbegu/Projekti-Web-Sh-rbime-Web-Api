using System.ComponentModel.DataAnnotations;
using Projekti1.M2MRelations;

namespace Projekti1.Models
{
    public class Dieta
    {
        [Key]
        public int Id { get; set; }
        public string Emri { get; set; } = string.Empty;
        public string Lloji { get; set; } = string.Empty;
        public string Pershkrimi { get; set; }
        public DateTime DataKrijimit { get; set; } = DateTime.Now;


        public ICollection<DietaUshqimi> DietaUshqimi { get; set; }

    }
}
