using Microsoft.AspNetCore.Mvc;
using PersonsAPI.DTOs;
using PersonsAPI.Services;

namespace PersonsApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PersonController : ControllerBase
{
    private readonly IPersonService _personService;

    public PersonController(IPersonService personService)
    {
        _personService = personService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var persons = await _personService.GetAllAsync();

        return Ok(new
        {
            total = persons.Count,
            data = persons
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var person = await _personService.GetByIdAsync(id);

        if (person == null)
        {
            return NotFound();
        }

        return Ok(person);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreatePersonDto dto)
    {
        var result = await _personService.CreateAsync(dto);

        if (result.Status == PersonServiceStatus.InvalidGender)
        {
            return BadRequest(new
            {
                message = "Invalid genderId.",
                genderId = dto.GenderId
            });
        }

        return Ok(new
        {
            message = "Person added successfully",
            person = result.Data
        });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        UpdatePersonDto dto)
    {
        var result = await _personService.UpdateAsync(id, dto);

        if (result.Status == PersonServiceStatus.NotFound)
        {
            return NotFound();
        }

        if (result.Status == PersonServiceStatus.InvalidGender)
        {
            return BadRequest(new
            {
                message = "Invalid genderId.",
                genderId = dto.GenderId
            });
        }

        return Ok(new
        {
            message = "Person updated successfully",
            id = result.Data?.PersonId,
            person = result.Data
        });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _personService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return Ok(new
        {
            message = "Person deleted successfully"
        });
    }

    [HttpGet("adults")]
    public async Task<IActionResult> GetAdults()
    {
        var adults = await _personService.GetAdultsAsync();

        return Ok(new
        {
            total = adults.Count,
            data = adults
        });
    }
}