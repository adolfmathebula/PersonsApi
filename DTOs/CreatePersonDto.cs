using System.ComponentModel.DataAnnotations;
using PersonsAPI.Models;

namespace PersonsAPI.DTOs;

// DTO for creating a new person
public class CreatePersonDto
{
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

    [Range(1, int.MaxValue)]
    public int GenderId { get; set; }
}