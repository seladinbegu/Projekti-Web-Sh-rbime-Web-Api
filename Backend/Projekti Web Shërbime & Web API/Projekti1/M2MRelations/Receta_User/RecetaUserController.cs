using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projekti1.Data;
using Projekti1.M2MRelations.Receta_User;
using Projekti1.M2MRelations.Receta_User.DTOs;
using Projekti1.M2MRelations.Receta_Ushqimi.DTOs;
using Projekti1.Receta.Data;
using Projekti1.User.Data;



[ApiController]
[Route("api/[controller]")]
public class RecetaUserController : ControllerBase
{
    private readonly Receta_UserDbContext _context;  // Dieta_UshqimiDbContext
    private readonly RecetaDbContext _context2;         // DietaDbContext
    private readonly UserDbContext _context3;      // UshqimiDbContext

    // Constructor with dependency injection for all DbContexts
    public RecetaUserController(Receta_UserDbContext context, RecetaDbContext context2, UserDbContext context3)
    {
        _context = context;
        _context2 = context2;
        _context3 = context3;
    }

    // GET: api/DietaUshqimi
    [HttpGet]
    public IActionResult Get()
    {
        var recetauser = _context.RecetaUser
            .ToList()
            .Select(s => s.toRecetaUserDto())  // Assuming mapping method exists
            .ToList();  // Ensures it's materialized before returning
        return Ok(recetauser);
    }

    // Get diet by ID (DietaId and UshqimiId)
    [HttpGet("{recetaId}/{userId}")]
    public async Task<IActionResult> Get(int recetaId, string userId)
    {
        var recetaUser = await _context.RecetaUser
            .FirstOrDefaultAsync(du => du.RecetaId == recetaId && du.UserId == userId);

        if (recetaUser == null)
        {
            return NotFound();
        }

        return Ok(recetaUser.toRecetaUserDto());  // Assuming mapping method exists
    }

    [HttpGet("user/{userId}")]
    public IActionResult GetRecetaUserByUserId(string userId)
    {
        // Use .Where() to filter by userId and ensure it's compared correctly.
        var recetaUsers = _context.RecetaUser
            .Where(ru => ru.UserId == userId) // Ensures that the comparison is case-sensitive and accurate
            .ToList();

        // If no recetaUser records are found, return null (or an empty array, depending on preference)
        if (!recetaUsers.Any())
        {
            return Ok(null);  // Return null if no records are found
        }

        return Ok(recetaUsers);  // Return 200 with the list of recetaUser records
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RecetaUserCreateDto recetauserDto)
    {
        // Validate if the referenced Receta and User exist
        var receta = await _context2.Receta.FindAsync(recetauserDto.RecetaId);
        var user = await _context3.User.FindAsync(recetauserDto.UserId);

        if (receta == null && user == null)
        {
            return BadRequest($"Receta with ID {recetauserDto.RecetaId} and User with ID {recetauserDto.UserId} not found.");
        }

        if (receta == null)
        {
            return BadRequest($"Receta with ID {recetauserDto.RecetaId} not found.");
        }

        if (user == null)
        {
            return BadRequest($"User with ID {recetauserDto.UserId} not found.");
        }

        // Check for duplicates (same RecetaId and UserId)
        var existingRecetaUser = await _context.RecetaUser
            .FirstOrDefaultAsync(r => r.RecetaId == recetauserDto.RecetaId && r.UserId == recetauserDto.UserId);

        if (existingRecetaUser != null)
        {
            return BadRequest($"A record with RecetaId {recetauserDto.RecetaId} and UserId {recetauserDto.UserId} already exists.");
        }

        // Directly insert the new record
        var recetauserModel = recetauserDto.toRecetaUserFromCreateDto();
        _context.RecetaUser.Add(recetauserModel);

        await _context.SaveChangesAsync();  // Save changes to database

        // Return the created data with a 200 OK status
        return Ok();
    }



    // Delete a diet by ID
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        // Find the DietaUshqimi entry by its Id
        var recetaUser = await _context.RecetaUser
            .FirstOrDefaultAsync(du => du.Id == id);

        if (recetaUser == null)
        {
            return NotFound("The relationship between Dieta and Ushqimi does not exist.");
        }

        // Remove the entry from the join table
        _context.RecetaUser.Remove(recetaUser);
        await _context.SaveChangesAsync();  // Use SaveChangesAsync for async behavior

        return NoContent();  // 204 No Content - successful deletion
    }

}
