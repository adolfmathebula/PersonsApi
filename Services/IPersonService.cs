using PersonsAPI.DTOs;
using PersonsAPI.Models;

namespace PersonsAPI.Services;

public interface IPersonService
{
    Task<List<Person>> GetAllAsync();

    Task<Person?> GetByIdAsync(int id);

    Task<PersonServiceResult<Person>> CreateAsync(CreatePersonDto dto);

    Task<PersonServiceResult<Person>> UpdateAsync(int id, UpdatePersonDto dto);

    Task<bool> DeleteAsync(int id);

    Task<List<AdultPerson>> GetAdultsAsync();
}