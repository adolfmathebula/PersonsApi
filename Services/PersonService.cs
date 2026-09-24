using Microsoft.EntityFrameworkCore;
using PersonsAPI.Data;
using PersonsAPI.DTOs;
using PersonsAPI.Models;

namespace PersonsAPI.Services;

public class PersonService : IPersonService
{
    private readonly PersonsDbContext _context;

    public PersonService(PersonsDbContext context)
    {
        _context = context;
    }

    public async Task<List<Person>> GetAllAsync()
    {
        return await _context.Persons
            .Include(p => p.Gender)
            .ToListAsync();
    }

    public async Task<Person?> GetByIdAsync(int id)
    {
        return await _context.Persons
            .Include(p => p.Gender)
            .FirstOrDefaultAsync(p => p.PersonId == id);
    }

    public async Task<PersonServiceResult<Person>> CreateAsync(
        CreatePersonDto dto)
    {
        var genderExists = await _context.Genders
            .AnyAsync(g => g.GenderId == dto.GenderId);

        if (!genderExists)
        {
            return new PersonServiceResult<Person>(
                PersonServiceStatus.InvalidGender);
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

        var createdPerson = await GetByIdAsync(person.PersonId);

        return new PersonServiceResult<Person>(
            PersonServiceStatus.Success,
            createdPerson);
    }

    public async Task<PersonServiceResult<Person>> UpdateAsync(
        int id,
        UpdatePersonDto dto)
    {
        var existingPerson = await _context.Persons
            .FindAsync(id);

        if (existingPerson == null)
        {
            return new PersonServiceResult<Person>(
                PersonServiceStatus.NotFound);
        }

        var genderExists = await _context.Genders
            .AnyAsync(g => g.GenderId == dto.GenderId);

        if (!genderExists)
        {
            return new PersonServiceResult<Person>(
                PersonServiceStatus.InvalidGender);
        }

        existingPerson.FirstName = dto.FirstName;
        existingPerson.LastName = dto.LastName;
        existingPerson.DateOfBirth = dto.DateOfBirth!.Value;
        existingPerson.Email = dto.Email;
        existingPerson.Phone = dto.Phone;
        existingPerson.GenderId = dto.GenderId;

        await _context.SaveChangesAsync();

        var updatedPerson = await GetByIdAsync(id);

        return new PersonServiceResult<Person>(
            PersonServiceStatus.Success,
            updatedPerson);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var person = await _context.Persons
            .FindAsync(id);

        if (person == null)
        {
            return false;
        }

        _context.Persons.Remove(person);

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<List<AdultPerson>> GetAdultsAsync()
    {
        return await _context.AdultPersons
            .ToListAsync();
    }
}