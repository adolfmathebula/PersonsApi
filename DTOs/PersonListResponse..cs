namespace PersonsAPI.DTOs;

public class PersonListResponse<T>
{
    public int Total { get; set; }
    public List<T> Data { get; set; } = new();
}