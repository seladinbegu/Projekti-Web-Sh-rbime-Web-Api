using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Projekti1.Data;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Authorization;

namespace Projekti1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]

    public class UshqimiController : ControllerBase
    {
        private readonly UshqimiDbContext _context;

        public UshqimiController(UshqimiDbContext context)
        {
            _context = context;
        }

        // GET: api/Ushqimi
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            var ushqimi = await _context.Ushqimi.ToListAsync();
            var result = ushqimi.Select(s => s.toUshqimiDto());
            return Ok(result);
        }

        // GET: api/Ushqimi/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var ushqimi = await _context.Ushqimi.FindAsync(id);

            if (ushqimi == null)
            {
                return NotFound();
            }
            return Ok(ushqimi.toUshqimiDto());
        }

        [HttpPost]
        public IActionResult Create([FromBody] UshqimiCreateDto ushqimiDto)
        {
            // Directly map and save the new diet
            var ushqimiModel = ushqimiDto.toUshqimiFromCreateDto();
            _context.Ushqimi.Add(ushqimiModel);
            _context.SaveChanges();

            // Return the created diet's data with a 201 Created status
            return CreatedAtAction(nameof(GetById), new { id = ushqimiModel.Id }, ushqimiModel.toUshqimiDto());
        }

        // PUT: api/Ushqimi/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UshqimiUpdateDto updateDto)
        {
            if (updateDto == null)
            {
                return BadRequest("Update data is invalid.");
            }

            var ushqimiModel = await _context.Ushqimi.FirstOrDefaultAsync(b => b.Id == id);
            if (ushqimiModel == null)
            {
                return NotFound();
            }

            // Update properties from updateDto
            ushqimiModel.Emri = updateDto.Emri;
            ushqimiModel.Kalori = updateDto.Kalori;
            ushqimiModel.Proteina = updateDto.Proteina;
            ushqimiModel.Karbohidrate = updateDto.Karbohidrate;
            ushqimiModel.Yndyrna = updateDto.Yndyrna;
            ushqimiModel.Fibrat = updateDto.Fibrat;
            ushqimiModel.VitaminC = updateDto.VitaminC;
            ushqimiModel.VitaminA = updateDto.VitaminA;
            ushqimiModel.Kalcium = updateDto.Kalcium;
            ushqimiModel.Hekur = updateDto.Hekur;
            ushqimiModel.Vegan = updateDto.Vegan;
            ushqimiModel.kaGluten = updateDto.kaGluten;
            ushqimiModel.kaBulmet = updateDto.kaBulmet;
            ushqimiModel.Kategoria = updateDto.Kategoria;
            ushqimiModel.Origjina = updateDto.Origjina;
            ushqimiModel.ImagePath = updateDto.ImagePath;


            // Save changes
            await _context.SaveChangesAsync();

            return Ok(ushqimiModel);
        }

        // DELETE: api/Ushqimi/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ushqimi = await _context.Ushqimi.FindAsync(id);

            if (ushqimi == null)
            {
                return NotFound();
            }

            _context.Ushqimi.Remove(ushqimi);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Ushqimi/upload

    }
}
