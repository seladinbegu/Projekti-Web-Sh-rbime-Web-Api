using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Projekti1.Models;
using Projekti1.Receta;


public class RecetaUshqimi
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  // Add this attribute to make the Id auto-increment
    public int Id { get; set; }

    public int RecetaId { get; set; }
    public Receta? Receta { get; set; }

    public int UshqimiId { get; set; }
    public Ushqimi? Ushqimi { get; set; }
}
