using ExpensesTracker.DTOs;
using ExpensesTracker.Services;
using Microsoft.AspNetCore.Authorization;
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
        [HttpPost("Login")]
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
                else
                {

                    return BadRequest("Invalid password");
                }
            }
            else
            {

                // ❌ If user doesn't exist, return a clear error message
                return BadRequest("User not found");
            }
            }
            [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO register)
        {
            if (!ModelState.IsValid) {
                return BadRequest("User already exsists");
            }
            var user = new IdentityUser
            {
                Email = register.Email,
                UserName = register.Username,

            };
            var exist =await  _userManager.FindByEmailAsync(user.Email);
            if (exist !=null) { return BadRequest("User Exsist"); }
            else
            {
                if (user == null) return BadRequest("Invalid User");
                var result = await _userManager.CreateAsync(user, register.Password);
                if (result.Succeeded) { return Ok(register.Username + "User Created Successfully"); }
                else { return BadRequest(result.Errors); }


            }
        }
        
    }
}
