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


    }
}
