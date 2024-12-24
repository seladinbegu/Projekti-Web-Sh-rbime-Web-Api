using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Projekti1.Receta;
using Projekti1.User;


public class RecetaUser
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  // Add this attribute to make the Id auto-increment
    public int Id { get; set; }

    public int RecetaId { get; set; }
    public Receta? Receta { get; set; }

    public string UserId { get; set; }  // Change this to string to match IdentityUser's Id type
    public User? User { get; set; }
}
