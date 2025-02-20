using Microsoft.AspNetCore.Mvc;
using olympo_webapi.Models;
using olympo_webapi.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public AuthController(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        if (loginRequest == null || string.IsNullOrWhiteSpace(loginRequest.Email) || string.IsNullOrWhiteSpace(loginRequest.Password))
        {
            return BadRequest(new { Message = "Invalid login request" });
        }

        var user = await _userRepository.GetByEmailAsync(loginRequest.Email);

        if (user == null)
        {
            return Unauthorized(new { Message = "User is Null" });
        }

        var passwordVerificationResult = HashService.PasswordVerificationResult(user.Password, loginRequest.Password);

        if (passwordVerificationResult == PasswordVerificationResult.Success)
        {
            HttpContext.Session.SetString("UserId", user.Id.ToString());

            return Ok(new 
            { 
                Message = "Login successful", 
                User = new 
                {
                    user.Id,
                    user.Email,
                    user.Name,
                    user.Type
                } 
            });
        } 
        else
        {

            return Unauthorized(new { Message = "Invalid email or password" });
        }
    
    }

    [AllowAnonymous]
    [HttpPost("logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return Ok(new { Message = "Logged out successfully" });
    }

}
public class LoginRequest
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
