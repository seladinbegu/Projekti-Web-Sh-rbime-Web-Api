using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Projekti1.Data;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using System;

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
            // Convert the DTO to the model
            var ushqimiModel = ushqimiDto.toUshqimiFromCreateDto();

            // Check if ImagePath is provided and update accordingly
            if (!string.IsNullOrEmpty(ushqimiDto.ImagePath))
            {
                ushqimiModel.ImagePath = "images/" + ushqimiDto.ImagePath; // Store relative path
            }

            // Add the model to the database and save changes
            _context.Ushqimi.Add(ushqimiModel);
            _context.SaveChanges();

            // Return the created resource with the appropriate response
            return CreatedAtAction(nameof(GetById), new { id = ushqimiModel.Id }, ushqimiModel.toUshqimiDto());
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UshqimiUpdateDto updateDto)
        {
            var ushqimiModel = _context.Ushqimi.FirstOrDefault(b => b.Id == id);
            if (ushqimiModel == null)
            {
                return NotFound();
            }

            // Update the properties from updateDto
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

            // Handle the image path update if provided
            if (!string.IsNullOrEmpty(updateDto.ImagePath))
            {
                ushqimiModel.ImagePath = "images/" + updateDto.ImagePath; // Store relative path
            }

            // Save changes
            _context.SaveChanges();

            return Ok(ushqimiModel);
        }

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

            return NoContent();
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

            if (!Directory.Exists(uploadDirectory))
            {
                Directory.CreateDirectory(uploadDirectory);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadDirectory, fileName);

            try
            {
                // Save the file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Return the relative path to the image
                var imagePath = Path.Combine("images", fileName); // This is the relative path
                return Ok(new { ImagePath = imagePath });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
