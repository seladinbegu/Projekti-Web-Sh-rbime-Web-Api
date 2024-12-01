using Microsoft.AspNetCore.Mvc;
using Projekti1.Data;

namespace Projekti1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UshqimiController : ControllerBase
    {
        private readonly UshqimiDbContext _context;

        public UshqimiController(UshqimiDbContext context)
        {
            _context = context;
        }









        [HttpGet]
        public IActionResult Get()
        {
            var ushqimi = _context.Ushqimi.ToList().Select(s => s.toUshqimiDto());
            return Ok(ushqimi);
        }





        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var ushqimi = _context.Ushqimi.Find(id);

            if (ushqimi == null)
            {
                return NotFound();
            }
            return Ok(ushqimi.toUshqimiDto());

        }








        [HttpPost]
        public IActionResult Create([FromBody] UshqimiCreateDto ushqimiDto)
        {
            // Directly map and save the new book
            var ushqimiModel = ushqimiDto.toUshqimiFromCreateDto();
            _context.Ushqimi.Add(ushqimiModel);
            _context.SaveChanges();

            // Return the created book's data with a 201 Created status
            return CreatedAtAction(nameof(GetById), new { id = ushqimiModel.Id }, ushqimiModel.toUshqimiDto());
        }


        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UshqimiUpdateDto updateDto)
        {
            // Retrieve the existing Ushqimi item from the database
            var ushqimiModel = _context.Ushqimi.FirstOrDefault(b => b.Id == id);
            if (ushqimiModel == null)
            {
                return NotFound(); // Return 404 if the item is not found
            }

            // Update the properties from the DTO
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
            ushqimiModel.Pershkrimi = updateDto.Pershkrimi;

            // Optionally update the DataKrijimit if needed (e.g., to not overwrite)
            // ushqimiModel.DataKrijimit = DateTime.Now; // If you want to change it

            // Save changes to the database
            _context.SaveChanges();

            // Return the updated model as a response (optionally, using a DTO for the response)
            return Ok(ushqimiModel); // Or `return Ok(ushqimiModel.toDietaDto());` if using a DTO for the response
        }


        // Delete a diet by ID
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var ushqimi = _context.Ushqimi.Find(id);

            if (ushqimi == null)
            {
                return NotFound();
            }

            _context.Ushqimi.Remove(ushqimi);
            _context.SaveChanges();

            return NoContent(); // 204 No Content for successful delete
        }









    }
}
