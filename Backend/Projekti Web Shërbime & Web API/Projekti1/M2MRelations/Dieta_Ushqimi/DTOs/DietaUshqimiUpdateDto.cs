using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekti1.M2MRelations.Dieta_Ushqimi.DTOs
{
    public class DietaUshqimiUpdateDto
    {
        public int Id { get; set; }
        public int UshqimiId { get; set; }
        public int DietaId { get; set; }
    }
}