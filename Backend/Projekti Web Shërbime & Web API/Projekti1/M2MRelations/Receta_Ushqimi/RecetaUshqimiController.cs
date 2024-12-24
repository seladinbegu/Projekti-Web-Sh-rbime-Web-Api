using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projekti1.Data;
using Projekti1.M2MRelations.Receta_Ushqimi.DTOs;
using Projekti1.Receta.Data;

namespace Projekti1.M2MRelations.Receta_Ushqimi
{

    [ApiController]
    [Route("api/[controller]")]
    public class RecetaUshqimiController : ControllerBase
    {
        private readonly Receta_UshqimiDbContext _context;  // Dieta_UshqimiDbContext
        private readonly RecetaDbContext _context2;         // DietaDbContext
        private readonly UshqimiDbContext _context3;      // UshqimiDbContext

        // Constructor with dependency injection for all DbContexts
        public RecetaUshqimiController(Receta_UshqimiDbContext context, RecetaDbContext context2, UshqimiDbContext context3)
        {
            _context = context;
            _context2 = context2;
            _context3 = context3;
        }

        // GET: api/DietaUshqimi
        [HttpGet]
        public IActionResult Get()
        {
            var recetaushqimi = _context.RecetaUshqimi
                .ToList()
                .Select(s => s.toRecetaUshqimiDto())  // Assuming mapping method exists
                .ToList();  // Ensures it's materialized before returning
            return Ok(recetaushqimi);
        }

        // Get diet by ID (DietaId and UshqimiId)
        [HttpGet("{recetaId}/{ushqimiId}")]
        public async Task<IActionResult> Get(int recetaId, int ushqimiId)
        {
            var recetaUshqimi = await _context.RecetaUshqimi
                .FirstOrDefaultAsync(du => du.RecetaId == recetaId && du.UshqimiId == ushqimiId);

            if (recetaUshqimi == null)
            {
                return NotFound();
            }

            return Ok(recetaUshqimi.toRecetaUshqimiDto());  // Assuming mapping method exists
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RecetaUshqimiCreateDto recetaushqimiDto)
        {
            // Validate if the referenced Dieta and Ushqimi exist
            var receta = await _context2.Receta.FindAsync(recetaushqimiDto.RecetaId);
            var ushqimi = await _context3.Ushqimi.FindAsync(recetaushqimiDto.UshqimiId);

            if (receta == null && ushqimi == null)
            {
                return BadRequest($"Receta with ID {recetaushqimiDto.RecetaId} and Ushqimi with ID {recetaushqimiDto.UshqimiId} not found.");
            }

            if (receta == null)
            {
                return BadRequest($"Receta with ID {recetaushqimiDto.RecetaId} not found.");
            }

            if (ushqimi == null)
            {
                return BadRequest($"Ushqimi with ID {recetaushqimiDto.UshqimiId} not found.");
            }

            // Check for duplicates (same RecetaId and UshqimiId)
            var existingRecetaUshqimi = await _context.RecetaUshqimi
                .FirstOrDefaultAsync(r => r.RecetaId == recetaushqimiDto.RecetaId && r.UshqimiId == recetaushqimiDto.UshqimiId);

            if (existingRecetaUshqimi != null)
            {
                return BadRequest($"A record with RecetaId {recetaushqimiDto.RecetaId} and UshqimiId {recetaushqimiDto.UshqimiId} already exists.");
            }

            // Directly insert the new record
            var recetaushqimiModel = recetaushqimiDto.toRecetaUshqimiFromCreateDto();
            _context.RecetaUshqimi.Add(recetaushqimiModel);

            await _context.SaveChangesAsync();  // Save changes to the database

            // Return the created data with a 200 OK status
            return Ok();
        }



        // Delete a diet by ID
        [HttpDelete("{recetaId}/{ushqimiId}")]
        public async Task<IActionResult> Delete(int recetaId, int ushqimiId)
        {
            // Find the RecetaUshqimi entry by RecetaId and UshqimiId
            var recetaUshqimi = await _context.RecetaUshqimi
                .FirstOrDefaultAsync(du => du.RecetaId == recetaId && du.UshqimiId == ushqimiId);

            if (recetaUshqimi == null)
            {
                return NotFound("The relationship between Receta and Ushqimi does not exist.");
            }

            // Remove the entry from the join table
            _context.RecetaUshqimi.Remove(recetaUshqimi);
            await _context.SaveChangesAsync();  // Use SaveChangesAsync for async behavior

            return NoContent();  // 204 No Content - successful deletion
        }

    }
}
