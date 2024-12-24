using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Projekti1.Receta.DTOs;

namespace Projekti1.Receta
{
    public static class RecetaMapper
    {
        public static RecetaDto toRecetaDto(this Receta recetaModel)
        {
            return new RecetaDto
            {
                Id = recetaModel.Id,
                Emri = recetaModel.Emri,

                Udhezimet = recetaModel.Udhezimet,
                DataKrijimit = recetaModel.DataKrijimit
            };
        }

        public static Receta toRecetaFromCreateDto(this RecetaCreateDto recetaDto)
        {
            return new Receta
            {
                Emri = recetaDto.Emri,
                Udhezimet = recetaDto.Udhezimet,
                DataKrijimit = DateTime.Now // Automatically set to cu
            };
        }
    }
}