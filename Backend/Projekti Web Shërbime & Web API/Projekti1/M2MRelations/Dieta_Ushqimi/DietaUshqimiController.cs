using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projekti1.Data;
using Projekti1.M2MRelations;
using Projekti1.M2MRelations.Data;
using Projekti1.M2MRelations.Dieta_Ushqimi;
using Projekti1.M2MRelations.Dieta_Ushqimi.DTOs;
using System.Linq;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class DietaUshqimiController : ControllerBase
{
    private readonly Dieta_UshqimiDbContext _context;  // Dieta_UshqimiDbContext
    private readonly DietaDbContext _context2;         // DietaDbContext
    private readonly UshqimiDbContext _context3;      // UshqimiDbContext

    // Constructor with dependency injection for all DbContexts
    public DietaUshqimiController(Dieta_UshqimiDbContext context, DietaDbContext context2, UshqimiDbContext context3)
    {
        _context = context;
        _context2 = context2;
        _context3 = context3;
    }

    // GET: api/DietaUshqimi
    [HttpGet]
    public IActionResult Get()
    {
        var dietaushqimi = _context.DietaUshqimi
            .ToList()
            .Select(s => s.toDietaUshqimiDto())  // Assuming mapping method exists
            .ToList();  // Ensures it's materialized before returning
        return Ok(dietaushqimi);
    }

    // Get diet by ID (DietaId and UshqimiId)
    [HttpGet("{dietaId}/{ushqimiId}")]
    public async Task<IActionResult> Get(int dietaId, int ushqimiId)
    {
        var dietaUshqimi = await _context.DietaUshqimi
            .FirstOrDefaultAsync(du => du.DietaId == dietaId && du.UshqimiId == ushqimiId);

        if (dietaUshqimi == null)
        {
            return NotFound();
        }

        return Ok(dietaUshqimi.toDietaUshqimiDto());  // Assuming mapping method exists
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] DietaUshqimiCreateDto dietaushqimiDto)
    {
        // Validate if the referenced Dieta and Ushqimi exist
        var dieta = await _context2.Dieta.FindAsync(dietaushqimiDto.DietaId);
        var ushqimi = await _context3.Ushqimi.FindAsync(dietaushqimiDto.UshqimiId);

        if (dieta == null && ushqimi == null)
        {
            return BadRequest($"Dieta with ID {dietaushqimiDto.DietaId} and Ushqimi with ID {dietaushqimiDto.UshqimiId} not found.");

        }

        if (dieta == null)
        {
            return BadRequest($"Dieta with ID {dietaushqimiDto.DietaId} not found.");
        }

        if (ushqimi == null)
        {
            return BadRequest($"Ushqimi with ID {dietaushqimiDto.UshqimiId} not found.");
        }

        // Directly insert the new record
        var dietaushqimiModel = dietaushqimiDto.toDietaUshqimiFromCreateDto();
        _context.DietaUshqimi.Add(dietaushqimiModel);

        await _context.SaveChangesAsync();  // Save changes to database

        // Return the created data with a 200 OK status
        return Ok();
    }


    // Delete a diet by ID
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        // Find the DietaUshqimi entry by its Id
        var dietaUshqimi = await _context.DietaUshqimi
            .FirstOrDefaultAsync(du => du.Id == id);

        if (dietaUshqimi == null)
        {
            return NotFound("The relationship between Dieta and Ushqimi does not exist.");
        }

        // Remove the entry from the join table
        _context.DietaUshqimi.Remove(dietaUshqimi);
        await _context.SaveChangesAsync();  // Use SaveChangesAsync for async behavior

        return NoContent();  // 204 No Content - successful deletion
    }
}
