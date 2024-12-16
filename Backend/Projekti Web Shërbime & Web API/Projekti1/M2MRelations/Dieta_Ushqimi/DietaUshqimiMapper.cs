using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Projekti1.M2MRelations.Dieta_Ushqimi.DTOs;
using Projekti1.Models;

namespace Projekti1.M2MRelations.Dieta_Ushqimi
{
    public static class DietaUshqimiMapper
    {
        public static DietaUshqimiDto toDietaUshqimiDto(this DietaUshqimi dietaushqimiModel)
        {
            return new DietaUshqimiDto
            {
                Id = dietaushqimiModel.Id,
                UshqimiId = dietaushqimiModel.UshqimiId,
                DietaId = dietaushqimiModel.DietaId,
            };
        }

        public static DietaUshqimi toDietaUshqimiFromCreateDto(this DietaUshqimiCreateDto dietaushqimiDto)
        {
            return new DietaUshqimi
            {
                UshqimiId = dietaushqimiDto.UshqimiId,
                DietaId = dietaushqimiDto.DietaId,

            };
        }
    }
}