using System.ComponentModel.DataAnnotations;

namespace Projekti1.Models
{
    public class Receta
    {
        [Key]
        public int Id { get; set; }
        public string Emri { get; set; } = string.Empty;
        public string Udhezimet { get; set; }
        public DateTime DataKrijimit { get; set; }
    }
}
