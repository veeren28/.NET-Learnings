using DTOpractice.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using AutoMapper;
namespace DTOpractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppContextDb _context;
        private readonly IMapper _Mapping;
        public UserController(AppContextDb context , IMapper Mapping)
        {
            _context = context;
            _Mapping = Mapping;
        }
        //[HttpGet]
        //public IActionResult Get()
        //{
        //    var users = _context.Users.ToList();
        //    return Ok(users);
        //}
        //[HttpPost("Post")]
        //public async Task<IActionResult> Post([FromBody]UserModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);

        //    }
        //    await _context.Users.AddAsync(model);
        //    await _context.SaveChangesAsync();
        //    return CreatedAtAction(nameof(Get), new { id = model.Id }, model);

        //}



        //                   Using     DTO  
        //[HttpGet]
        //public async Task<IActionResult> Get()
        //{
        //    var user  =  await _context.Users
        //        .Select(
        //        u => new UserDTO
        //        {
        //           UserName = u.Name,
        //           Email = u.Email,
        //        })
        //        .ToListAsync();
        //    return Ok(user);

        //}
        //[HttpPost("Post")]
        //public async Task<IActionResult> Post( CreateUserDTO userData)
        //{
        //    var userP = new UserModel
        //    {
        //        Name = userData.UserName,
        //        Password = userData.Password,
        //        Email = userData.Email,

        //    };
        //    await _context.Users.AddAsync(userP);
        //    await _context.SaveChangesAsync();
        //    return Ok(userP);
        //}



        //                USING AUTOMAPPING 

        [HttpGet]
        public async Task<IActionResult> Get() {
            var users = await _context.Users.ToListAsync();
            var dto = _Mapping.Map<List<UserDTO>>(users);
            return Ok(dto);

        }
        [HttpPost("Post")]
       
        public async Task<IActionResult> Post(CreateUserDTO userDet)
        {
            // Step 1: Map incoming CreateUserDTO to UserModel (your database entity)
            var dto = _Mapping.Map<UserModel>(userDet);

            // Step 2: Add the user to the database
            await _context.Users.AddAsync(dto);

            // Step 3: Save the changes so that EF generates the Id and persists the user
            await _context.SaveChangesAsync();

            // Step 4: Map the saved UserModel back to a DTO for safe response
            var user = _Mapping.Map<UserDTO>(dto);

            // Step 5: Return the DTO (e.g., without password or other private fields)
            return Ok(user);
        }


    }
}
