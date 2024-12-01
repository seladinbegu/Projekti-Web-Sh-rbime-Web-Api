using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Projekti1.Models
{
    public class Ushqimi
    {
        [Key]
        public int Id { get; set; }
        public string Emri { get; set; } = string.Empty;
        public double Kalori { get; set; }
        public double Proteina { get; set; }
        public double Karbohidrate { get; set; }
        public double Yndyrna { get; set; }
        public double Fibrat { get; set; }
        public double VitaminC { get; set; }
        public double VitaminA { get; set; }
        public double Kalcium { get; set; }
        public double Hekur { get; set; }
        public bool Vegan { get; set; }
        public bool kaGluten { get; set; }
        public bool kaBulmet { get; set; }
        public string Kategoria { get; set; }
        public string Pershkrimi { get; set; }
        public DateTime DataKrijimit { get; set; } = DateTime.Now;
    }
}
