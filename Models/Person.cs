using System.ComponentModel.DataAnnotations;

namespace PersonsAPI.Models;

public class Person
{
    public int PersonId { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [DateOfBirth]
    public DateTime? DateOfBirth { get; set; }

    [EmailAddress]
    [StringLength(100)]
    public string? Email { get; set; }

    [StringLength(20)]
    public string? Phone { get; set; }

    [Required]
    public int GenderId { get; set; }

    // public Gender Gender { get; set; } = null!;
    public Gender? Gender { get; set; }
}