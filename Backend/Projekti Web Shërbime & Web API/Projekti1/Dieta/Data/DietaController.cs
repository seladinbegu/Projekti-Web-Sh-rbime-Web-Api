using Microsoft.AspNetCore.Mvc;
using Projekti1.Data;

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









        [HttpGet]
        public IActionResult Get()
        {
            var dieta = _context.Dieta.ToList().Select(s => s.toDietaDto());
            return Ok(dieta);
        }





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








        [HttpPost]
        public IActionResult Create([FromBody] DietaCreateDto dietaDto)
        {
            // Directly map and save the new book
            var dietaModel = dietaDto.toDietaFromCreateDto();
            _context.Dieta.Add(dietaModel);
            _context.SaveChanges();

            // Return the created book's data with a 201 Created status
            return CreatedAtAction(nameof(GetById), new { id = dietaModel.Id }, dietaModel.toDietaDto());
        }


    }
}
