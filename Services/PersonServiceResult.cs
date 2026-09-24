namespace PersonsAPI.Services;

public enum PersonServiceStatus
{
    Success, //200
    NotFound, //404
    InvalidGender //400
}

public class PersonServiceResult<T>
{
    public PersonServiceStatus Status { get; set; }

    public T? Data { get; set; }

    public PersonServiceResult(PersonServiceStatus status, T? data = default)
    {
        Status = status;
        Data = data;
    }
}