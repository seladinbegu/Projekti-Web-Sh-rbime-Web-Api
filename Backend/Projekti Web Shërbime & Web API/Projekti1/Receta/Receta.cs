using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Projekti1.Receta
{
    public class Receta
    {
        [Key]
        public int Id { get; set; }
        public string? Emri { get; set; }
        public string? Udhezimet { get; set; }
        public DateTime DataKrijimit { get; set; } = DateTime.Now;



        public ICollection<RecetaUshqimi>? RecetaUshqimi { get; set; }

    }
}