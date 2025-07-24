using ExpensesTracker.Data;
using ExpensesTracker.DTOs;
using ExpensesTracker.Models;
using ExpensesTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ExpensesTracker.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly AppContextDb _context;
        private readonly UserManager<UserApplication> userManager;
        private readonly ILogger<HomeController> _logger;
      
        public HomeController(AppContextDb context, ILogger<HomeController> logger)
        {
            _context = context;
          _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _logger.LogInformation("UserId: {UserId}", userId);
            if (userId != null)
            {
                var list  =  await _context.Expenses.Where(e=> e.UserId==userId).ToListAsync();
                return Ok(list);
            }
           
                return Unauthorized();
        }
        
        [HttpPost("AddItem")]
        public async Task<IActionResult> AddExpense(AddItemDTO itemDTO)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _context.Users.FindAsync(userId);
            if (user == null) { return BadRequest("User Not Found"); }
            if (user.Balance < itemDTO.Amount) { return Ok("Insufficient Balance"); }
            user.Balance -= itemDTO.Amount;

            // user is a predefined term 
            
            var category = await _context.Category.FirstOrDefaultAsync(e => e.CategoryName==itemDTO.CategoryName);
            if (category == null) { return NotFound("Category Not Found"); }
            var item = new ExpensesModel
            {

                Title = itemDTO.Title,
                Description = itemDTO.Description,
                Date = DateTime.Now,
                Amount = itemDTO.Amount,
                CategoryId = category.Id,
                UserId = userId,
                BalanceAfter = user.Balance

            };
            await _context.Expenses.AddAsync(item);
            await _context.SaveChangesAsync();
            return Ok(item);
        }
        [HttpPut("EditItem/{id}")]
        public async Task<IActionResult> UpdateItem(int id,UpdateItemDTO itemDTO)
        {
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);contains Details of user 
            // it is not reuired since we are already authorized.


            var UpdateItem =await  _context.Expenses.FindAsync(id);
            if (UpdateItem == null) { return BadRequest("Id not found"); }

            UpdateItem.Title = itemDTO.Title;
            UpdateItem.Description = itemDTO.Description;
            UpdateItem.Date = itemDTO.Date;
           UpdateItem.UpdatedAt = DateTime.Now;

         
            UpdateItem.Amount = itemDTO.Amount;
        


            await _context.SaveChangesAsync();
            return Ok(UpdateItem);

        }
        [HttpDelete("Delete/{Id}")]
        public async Task<IActionResult> DeleteItem(int Id)
        {
            var delete = await _context.Expenses.FindAsync(Id);

            if (delete == null)
            {
                return BadRequest("Item not found");
            }

            _context.Expenses.Remove(delete);
            await _context.SaveChangesAsync();

            return Ok($"Item with ID {Id} {delete.Title} deleted successfully.");
            // Returns deleted object
        }


    }
}
