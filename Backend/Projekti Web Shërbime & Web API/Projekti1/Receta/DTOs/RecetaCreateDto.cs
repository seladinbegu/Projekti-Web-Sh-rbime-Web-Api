using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Projekti1.Receta.DTOs
{
    public class RecetaCreateDto
    {
        public string Emri { get; set; }
        public string Udhezimet { get; set; }
        [JsonIgnore]

        public DateTime DataKrijimit { get; set; } = DateTime.Now;
    }
}