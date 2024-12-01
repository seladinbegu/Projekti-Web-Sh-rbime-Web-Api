using Microsoft.AspNetCore.Mvc;
using Projekti1.Data;
using Projekti1.Models;

namespace Projekti1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DietaController : ControllerBase
    {
        private readonly DietaDbContext _context;

        public DietaController(DietaDbContext context)
        {
            _context = context;
        }

        // Get all diets
        [HttpGet]
        public IActionResult Get()
        {
            var dieta = _context.Dieta.ToList().Select(s => s.toDietaDto());
            return Ok(dieta);
        }

        // Get diet by ID
        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var dieta = _context.Dieta.Find(id);

            if (dieta == null)
            {
                return NotFound();
            }
            return Ok(dieta.toDietaDto());
        }

        // Create a new diet
        [HttpPost]
        public IActionResult Create([FromBody] DietaCreateDto dietaDto)
        {
            // Directly map and save the new diet
            var dietaModel = dietaDto.toDietaFromCreateDto();
            _context.Dieta.Add(dietaModel);
            _context.SaveChanges();

            // Return the created diet's data with a 201 Created status
            return CreatedAtAction(nameof(GetById), new { id = dietaModel.Id }, dietaModel.toDietaDto());
        }

        // Update an existing diet

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] DietaUpdateDto updateDto)
        {
            var dietaModel = _context.Dieta.FirstOrDefault(b => b.Id == id);
            if (dietaModel == null)
            {
                return NotFound();
            }
            dietaModel.Emri = updateDto.Emri;
            dietaModel.Lloji = updateDto.Lloji;
            dietaModel.Pershkrimi = updateDto.Pershkrimi;


            _context.SaveChanges();
            return Ok(dietaModel.toDietaDto());
        }

        // Delete a diet by ID
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var dieta = _context.Dieta.Find(id);

            if (dieta == null)
            {
                return NotFound();
            }

            _context.Dieta.Remove(dieta);
            _context.SaveChanges();

            return NoContent(); // 204 No Content for successful delete
        }
    }
}
