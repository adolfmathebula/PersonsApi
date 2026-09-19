namespace PersonsAPI.Models;

public class Person
{
    public int PersonId { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public DateTime DateOfBirth { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public int GenderId { get; set; }

    public Gender Gender { get; set; } = null!;
}