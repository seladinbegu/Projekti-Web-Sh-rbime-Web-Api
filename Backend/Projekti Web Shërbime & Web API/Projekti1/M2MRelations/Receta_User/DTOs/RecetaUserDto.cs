using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekti1.M2MRelations.Receta_User.DTOs
{
    public class RecetaUserDto
    {
        public int Id { get; set; }
        public int RecetaId { get; set; }
        public string UserId { get; set; }
    }
}