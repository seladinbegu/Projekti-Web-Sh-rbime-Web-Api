using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Projekti1.M2MRelations.Receta_Ushqimi.DTOs;

namespace Projekti1.M2MRelations.Receta_Ushqimi
{
    public static class RecetaUshqimiMapper
    {
        public static RecetaUshqimiDto toRecetaUshqimiDto(this RecetaUshqimi recetaushqimiModel)
        {
            return new RecetaUshqimiDto
            {
                Id = recetaushqimiModel.Id,
                UshqimiId = recetaushqimiModel.UshqimiId,
                RecetaId = recetaushqimiModel.RecetaId,
            };
        }

        public static RecetaUshqimi toRecetaUshqimiFromCreateDto(this RecetaUshqimiCreateDto recetaushqimiDto)
        {
            return new RecetaUshqimi
            {
                UshqimiId = recetaushqimiDto.UshqimiId,
                RecetaId = recetaushqimiDto.RecetaId,

            };
        }
    }
}