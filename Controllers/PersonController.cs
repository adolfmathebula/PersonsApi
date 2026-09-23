using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonsAPI.Data;
using PersonsAPI.Models;
using PersonsAPI.DTOs;

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

        // return Ok(persons);

        return Ok(new
        {
            total = persons.Count,
            data = persons
        });
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
    public async Task<IActionResult> Update(int id, UpdatePersonDto dto)
    {
        var existingPerson = await _context.Persons.FindAsync(id);

        if (existingPerson == null)
        {
            return NotFound();
        }

        var genderExists = await _context.Genders
            .AnyAsync(g => g.GenderId == dto.GenderId);

        if (!genderExists)
        {
            return BadRequest(new
            {
                message = "Invalid genderId.",
                genderId = dto.GenderId
            });
        }

        existingPerson.FirstName = dto.FirstName;
        existingPerson.LastName = dto.LastName;
        existingPerson.DateOfBirth = dto.DateOfBirth!.Value;
        existingPerson.Email = dto.Email;
        existingPerson.Phone = dto.Phone;
        existingPerson.GenderId = dto.GenderId;

        await _context.SaveChangesAsync();

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
    public async Task<IActionResult> Create(CreatePersonDto dto)
    {
        // Validate gender
        var genderExists = await _context.Genders
            .AnyAsync(g => g.GenderId == dto.GenderId);

        if (!genderExists)
        {
            return BadRequest(new
            {
                message = "Invalid genderId.",
                genderId = dto.GenderId
            });
        }

        var person = new Person
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            DateOfBirth = dto.DateOfBirth!.Value,
            Email = dto.Email,
            Phone = dto.Phone,
            GenderId = dto.GenderId
        };

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

    [HttpGet("adults")]
    public async Task<IActionResult> GetAdults()
    {
        var adults = await _context.AdultPersons
            .ToListAsync();

        // return Ok(adults);

        return Ok(new
        {
            total = adults.Count,
            data = adults
        });
    }
}