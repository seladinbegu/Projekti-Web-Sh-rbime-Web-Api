using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Projekti1.M2MRelations.Receta_User.DTOs;

namespace Projekti1.M2MRelations.Receta_User
{
    public static class RecetaUserMapper
    {
        public static RecetaUserDto toRecetaUserDto(this RecetaUser recetauserModel)
        {
            return new RecetaUserDto
            {
                Id = recetauserModel.Id,
                UserId = recetauserModel.UserId,
                RecetaId = recetauserModel.RecetaId,
            };
        }

        public static RecetaUser toRecetaUserFromCreateDto(this RecetaUserCreateDto recetauserDto)
        {
            return new RecetaUser
            {
                UserId = recetauserDto.UserId,
                RecetaId = recetauserDto.RecetaId,

            };
        }
    }
}