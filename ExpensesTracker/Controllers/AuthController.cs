using ExpensesTracker.DTOs;
using ExpensesTracker.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ExpensesTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager; // Handles user-related actions (create, find, delete, etc.)
        private readonly SignInManager<IdentityUser> _signInManager; // Handles password verification and login checks
        private readonly GenerateJWTtoken _generateJWTtoken; // Our custom service that creates a JWT token

        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, GenerateJWTtoken generateJWTtoken)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _generateJWTtoken = generateJWTtoken;
        }

        // 👇 Handles POST requests to: /api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            // 🔍 Check if the user with the provided email exists
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);

            if (user != null)
            {
                // ✅ Verify the password (returns success/failure)
                // false = do not lockout the user on failure
                var result = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);

                if (result.Succeeded)
                {
                    // 🪪 If login is successful, generate a JWT token for this user
                    var token = _generateJWTtoken.CreateToken(user);

                    // ✅ We return a JSON object like { "token": "JWT_HERE" }
                    // It's better than just `return Ok(token);` because:
                    // - It gives a key to access the token easily in frontend (e.g., res.token)
                    // - Easier to extend in future (e.g., return email, role, etc.)
                    return Ok(new { token });
                }

                return BadRequest("Invalid password");
            }

            // ❌ If user doesn't exist, return a clear error message
            return BadRequest("User not found");
        }
    }
}
