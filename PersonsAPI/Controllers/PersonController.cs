using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonsAPI.Data;

namespace PersonsApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PersonController : ControllerBase
{
    private readonly PersonsDbContext _context;

    public PersonController(PersonsDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var persons = await _context.Persons
            .Include(p => p.Gender)
            .ToListAsync();

        return Ok(persons);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var person = await _context.Persons
            .Include(p => p.Gender)
            .FirstOrDefaultAsync(p => p.PersonId == id);

        if (person == null)
        {
            return NotFound();
        }
        return Ok(person);
    }
}