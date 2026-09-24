using PersonsAPI.DTOs;

namespace PersonsAPI.Services;

public interface IAuthService
{
    Task<bool> RegisterAsync(RegisterDto dto);

    Task<string?> LoginAsync(LoginDto dto);
}