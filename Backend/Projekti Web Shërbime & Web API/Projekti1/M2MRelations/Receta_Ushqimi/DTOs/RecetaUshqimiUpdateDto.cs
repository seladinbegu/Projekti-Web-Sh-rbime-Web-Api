using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekti1.M2MRelations.Receta_Ushqimi.DTOs
{
    public class RecetaUshqimiUpdateDto
    {
        public int Id { get; set; }
        public int RecetaId { get; set; }
        public int UshqimiId { get; set; }
    }
}