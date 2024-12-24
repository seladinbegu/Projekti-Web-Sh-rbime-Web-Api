using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projekti1.Data;
using Projekti1.Models;
using Projekti1.Receta;
using Projekti1.Receta.Data;
using Projekti1.Receta.DTOs;

namespace Projekti1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize(Roles = "Admin")]

    public class RecetaController : ControllerBase
    {
        private readonly RecetaDbContext _context;

        public RecetaController(RecetaDbContext context)
        {
            _context = context;
        }

        // Get all diets
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get()
        {
            var receta = _context.Receta.ToList().Select(s => s.toRecetaDto());
            return Ok(receta);
        }



        [HttpGet("emri/{id}")] // Define a unique route
        [AllowAnonymous]
        public IActionResult GetEmriById(int id)
        {
            // Fetch the recipe based on the provided ID
            var receta = _context.Receta.FirstOrDefault(r => r.Id == id);

            if (receta == null)
            {
                // Return a 404 Not Found response if the recipe doesn't exist
                return NotFound(new { message = "Receta not found" });
            }

            // Return only the "emri" of the recipe
            return Ok(new { emri = receta.Emri });
        }


        [HttpGet("emri")]
        [AllowAnonymous]
        public IActionResult GetEmriByIds([FromQuery] List<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                return BadRequest(new { message = "No recipe IDs provided." });
            }

            var recetas = _context.Receta.Where(r => ids.Contains(r.Id)).ToList();

            if (!recetas.Any())
            {
                return NotFound(new { message = "No recipes found." });
            }

            var recipeNames = recetas.Select(r => new { emri = r.Emri, id = r.Id }).ToList();
            return Ok(recipeNames);
        }






        // Get diet by ID
        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var receta = _context.Receta.Find(id);

            if (receta == null)
            {
                return NotFound();
            }
            return Ok(receta.toRecetaDto());
        }

        // Create a new diet
        [HttpPost]
        public IActionResult Create([FromBody] RecetaCreateDto recetaDto)
        {
            // Directly map and save the new diet
            var recetaModel = recetaDto.toRecetaFromCreateDto();
            _context.Receta.Add(recetaModel);
            _context.SaveChanges();

            // Return the created diet's data with a 201 Created status
            return CreatedAtAction(nameof(GetById), new { id = recetaModel.Id }, recetaModel.toRecetaDto());
        }

        // Update an existing diet

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] RecetaUpdateDto updateDto)
        {
            var recetaModel = _context.Receta.FirstOrDefault(b => b.Id == id);
            if (recetaModel == null)
            {
                return NotFound();
            }
            recetaModel.Emri = updateDto.Emri;
            recetaModel.Udhezimet = updateDto.Udhezimet;


            _context.SaveChanges();
            return Ok(recetaModel.toRecetaDto());
        }

        // Delete a diet by ID
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var receta = _context.Receta.Find(id);

            if (receta == null)
            {
                return NotFound();
            }

            _context.Receta.Remove(receta);
            _context.SaveChanges();

            return NoContent(); // 204 No Content for successful delete
        }
    }
}
