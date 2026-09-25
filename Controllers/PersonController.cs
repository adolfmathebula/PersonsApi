using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonsAPI.Authorization;
using PersonsAPI.DTOs;
using PersonsAPI.Models;
using PersonsAPI.Services;

namespace PersonsApi.Controllers;

[Authorize] // authenticated user is required to access the controller.
[ApiController]
[Route("api/[controller]")]

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

        return Ok(new ApiResponse<PersonListResponse<Person>>
        {
            Success = true,
            Message = "Persons retrieved successfully",
            Data = new PersonListResponse<Person>
            {
                Total = persons.Count,
                Data = persons
            }
        });
        /*
        return Ok(new
        {
            total = persons.Count,
            data = persons
        });
        */
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var person = await _personService.GetByIdAsync(id);

        if (person == null)
        {
            return Problem(
                statusCode: StatusCodes.Status404NotFound,
                title: "Person not found",
                detail: $"Person with ID {id} was not found.");
        }

        // return Ok(person);
        return Ok(new ApiResponse<Person>
        {
            Success = true,
            Message = "Person retrieved successfully",
            Data = person
        });
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreatePersonDto dto)
    {
        var result = await _personService.CreateAsync(dto);

        if (result.Status == PersonServiceStatus.InvalidGender)
        {
            return Problem(
                statusCode: StatusCodes.Status400BadRequest,
                title: "Invalid gender",
                detail: $"Gender ID {dto.GenderId} does not exist.");
        }

        return Ok(new ApiResponse<Person>
        {
            Success = true,
            Message = "Person added successfully",
            Data = result.Data
        });
    }

    [Authorize(Policy = AppPolicies.ManagerOrAdmin)]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        UpdatePersonDto dto)
    {
        var result = await _personService.UpdateAsync(id, dto);

        if (result.Status == PersonServiceStatus.NotFound)
        {
            return Problem(
                statusCode: StatusCodes.Status404NotFound,
                title: "Person not found",
                detail: $"Person with ID {id} was not found.");
        }

        if (result.Status == PersonServiceStatus.InvalidGender)
        {
            //return BadRequest(new
            //{
            //    message = "Invalid genderId.",
            //    genderId = dto.GenderId
            //});

            return Problem(
                statusCode: StatusCodes.Status400BadRequest,
                title: "Invalid gender",
                detail: $"Gender ID {dto.GenderId} does not exist.");
         }

        return Ok(new ApiResponse<Person>
        {
            Success = true,
            Message = "Person updated successfully",
            Data = result.Data
        });
    }

    // [Authorize(Roles = "Admin")]

    [Authorize(Policy = AppPolicies.AdminOnly)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _personService.DeleteAsync(id);

        if (!deleted)
        {
            return Problem(
                statusCode: StatusCodes.Status404NotFound,
                title: "Person not found",
                detail: $"Person with ID {id} was not found.");
        }

        return Ok(new ApiResponse<object>
        {
            Success = true,
            Message = "Person deleted successfully"
        });
    }

    [HttpGet("adults")]
    public async Task<IActionResult> GetAdults()
    {
        var adults = await _personService.GetAdultsAsync();

        return Ok(new ApiResponse<PersonListResponse<AdultPerson>>
        {
            Success = true,
            Message = "Adult persons retrieved successfully",
            Data = new PersonListResponse<AdultPerson>
            {
                Total = adults.Count,
                Data = adults
            }
        });
    }
}