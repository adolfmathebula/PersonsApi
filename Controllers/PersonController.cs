using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonsAPI.Data;
using PersonsAPI.Models;

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

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Person person)
    {
        if (id != person.PersonId)
        {
            return BadRequest();
        }

        var existingPerson = await _context.Persons.FindAsync(id);

        if (existingPerson == null)
        {
            return NotFound();
        }

        existingPerson.FirstName = person.FirstName;
        existingPerson.LastName = person.LastName;
        existingPerson.DateOfBirth = person.DateOfBirth;
        existingPerson.Email = person.Email;
        existingPerson.Phone = person.Phone;
        existingPerson.GenderId = person.GenderId;

        await _context.SaveChangesAsync();

        // return NoContent();

        var updatedPerson = await _context.Persons
        .Include(p => p.Gender)
        .FirstOrDefaultAsync(p => p.PersonId == id);

        return Ok(new
        {
            message = "Person updated successfully",
            id = updatedPerson?.PersonId,
            person = updatedPerson
        });
    }

    [HttpPost]
    public async Task<IActionResult> Create(Person person)
    {
        _context.Persons.Add(person);

        await _context.SaveChangesAsync();

        var createdPerson = await _context.Persons
            .Include(p => p.Gender)
            .FirstOrDefaultAsync(p => p.PersonId == person.PersonId);

        return Ok(new
        {
            message = "Person added successfully",
            person = createdPerson
        });
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var person = await _context.Persons.FindAsync(id);

        if (person == null)
        {
            return NotFound();
        }

        _context.Persons.Remove(person);

        await _context.SaveChangesAsync();

        return Ok(new
        {
            message = "Person deleted successfully"
        });
    }
}