using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using JWT_Practice;
using JWT_Practice.DTOs;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;



namespace JWT_Practice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;

        public HomeController(UserManager<ApplicationUser> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO register)
        {
            var user = new ApplicationUser
            {
                UserName = register.name,
                name = register.name,
                phno = register.phno
            };

            var result = await _userManager.CreateAsync(user, register.Password);

            if (result.Succeeded)
            {
                return Ok("User registered successfully");
            }

            return BadRequest(result.Errors);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO login)
        {
            var user = await _userManager.FindByNameAsync(login.name);
            if (user != null && await _userManager.CheckPasswordAsync(user, login.Password))
            {
                var token = GenerateToken(user);
                return Ok(new { Token = token });
            }

            return BadRequest("Invalid username or password");
        }

        public string GenerateToken(ApplicationUser user)
        {
            var jwtsettings = _config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtsettings["Key"]));

            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(ClaimTypes.NameIdentifier, user.Id)
    };

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtsettings["Issuer"],
                audience: jwtsettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(double.Parse(jwtsettings["ExpiresInMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}


