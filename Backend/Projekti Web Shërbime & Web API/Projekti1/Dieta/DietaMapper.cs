using System;
using Projekti1.Models; // Correct namespace

public static class DietaMapper
{
    public static DietaDto toDietaDto(this Dieta dietaModel)
    {
        return new DietaDto
        {
            Id = dietaModel.Id,
            Emri = dietaModel.Emri,
            Pershkrimi = dietaModel.Pershkrimi,
            Lloji = dietaModel.Lloji,
            DataKrijimit = dietaModel.DataKrijimit
        };
    }

    public static Dieta toDietaFromCreateDto(this DietaCreateDto dietaDto)
    {
        return new Dieta
        {
            Emri = dietaDto.Emri,
            Pershkrimi = dietaDto.Pershkrimi,
            Lloji = dietaDto.Lloji,
            DataKrijimit = dietaDto.DataKrijimit
        };
    }
}
