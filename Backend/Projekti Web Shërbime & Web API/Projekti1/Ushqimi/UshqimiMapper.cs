using System;
using Projekti1.Models;

public static class UshqimiMapper
{

    public static UshqimiDto toUshqimiDto(this Ushqimi ushqimiModel)
    {
        return new UshqimiDto
        {
            Id = ushqimiModel.Id,
            Emri = ushqimiModel.Emri,
            Kalori = ushqimiModel.Kalori,
            Proteina = ushqimiModel.Proteina,
            Karbohidrate = ushqimiModel.Karbohidrate,
            Yndyrna = ushqimiModel.Yndyrna,
            Fibrat = ushqimiModel.Fibrat,
            VitaminC = ushqimiModel.VitaminC,
            VitaminA = ushqimiModel.VitaminA,
            Kalcium = ushqimiModel.Kalcium,
            Hekur = ushqimiModel.Hekur,
            Vegan = ushqimiModel.Vegan,
            kaGluten = ushqimiModel.kaGluten,
            kaBulmet = ushqimiModel.kaBulmet,
            Kategoria = ushqimiModel.Kategoria,
            Origjina = ushqimiModel.Origjina,
            DataKrijimit = ushqimiModel.DataKrijimit,
            ImagePath = ushqimiModel.ImagePath
        };
    }

    public static Ushqimi toUshqimiFromCreateDto(this UshqimiCreateDto ushqimiDto)
    {
        return new Ushqimi
        {
            Emri = ushqimiDto.Emri,
            Kalori = ushqimiDto.Kalori,
            Proteina = ushqimiDto.Proteina,
            Karbohidrate = ushqimiDto.Karbohidrate,
            Yndyrna = ushqimiDto.Yndyrna,
            Fibrat = ushqimiDto.Fibrat,
            VitaminC = ushqimiDto.VitaminC,
            VitaminA = ushqimiDto.VitaminA,
            Kalcium = ushqimiDto.Kalcium,
            Hekur = ushqimiDto.Hekur,
            Vegan = ushqimiDto.Vegan,
            kaGluten = ushqimiDto.kaGluten,
            kaBulmet = ushqimiDto.kaBulmet,
            Kategoria = ushqimiDto.Kategoria,
            Origjina = ushqimiDto.Origjina,
            DataKrijimit = ushqimiDto.DataKrijimit,
            ImagePath = ushqimiDto.ImagePath
        };
    }


}
