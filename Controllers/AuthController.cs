using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonsAPI.DTOs;
using PersonsAPI.Services;

namespace PersonsApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [AllowAnonymous] //public access to this endpoint, without authentication.
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var created = await _authService.RegisterAsync(dto);

        if (!created)
        {
            return BadRequest(new
            {
                message = "Username already exists."
            });
        }

        return Ok(new
        {
            message = "User registered successfully."
        });
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var token = await _authService.LoginAsync(dto);

        if (token == null)
        {
            return Unauthorized(new
            {
                message = "Invalid username or password."
            });
        }

        return Ok(new
        {
            token
        });
    }
}