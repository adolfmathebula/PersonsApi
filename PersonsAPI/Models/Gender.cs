using System.Text.Json.Serialization;

namespace PersonsAPI.Models;

public class Gender
{
    public int GenderId { get; set; }

    public string Name { get; set; } = string.Empty;

    [JsonIgnore]
    public ICollection<Person> Persons { get; set; } = new List<Person>();
}