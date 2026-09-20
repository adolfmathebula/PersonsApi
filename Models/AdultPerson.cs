namespace PersonsAPI.Models
{
    public class AdultPerson
    {
        public int PersonId { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public int GenderId { get; set; }

        public string Gender { get; set; } = string.Empty;
    }
}
