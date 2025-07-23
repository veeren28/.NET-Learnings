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
        private readonly UserManager<IdentityUser> userManager;
      
        public HomeController(AppContextDb context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                //var list  =  await _context.Expenses.Where(e=> e.UserId.Equals(userId)).ToListAsync();
                return Ok("list");
            }
           
                return Unauthorized();
        }
        
        [HttpPost("AddItem")]
        public async Task<IActionResult> AddExpense(AddItemDTO itemDTO)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            // user is a predefined term 

            var item = new ExpensesModel
            {

                Title = itemDTO.Title,
                Description = itemDTO.Description,
                Date = DateTime.Now,
                Amount = itemDTO.Amount,
                Category = itemDTO.Category,
                UserId = userId

            };
            await _context.Expenses.AddAsync(item);
            await _context.SaveChangesAsync();
            return Ok(_context.Expenses.ToList());
        }
        [HttpPut("EditItem")]
        public async Task<IActionResult> UpdateItem(UpdateItemDTO itemDTO)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var UpdateItem = await _context.Expenses.FindAsync(itemDTO.Id);
            if (UpdateItem == null) { return BadRequest("Id not found"); }

            UpdateItem.Title = itemDTO.Title;
            UpdateItem.Description = itemDTO.Description;
            UpdateItem.Date = itemDTO.Date;
            UpdateItem.Amount = itemDTO.Amount;
            UpdateItem.UserId = userId;


            await _context.SaveChangesAsync();
            return Ok(UpdateItem + " sucessfully Updated");

        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteItem(int Id)
        {
            var delete = await _context.Expenses.FindAsync(Id);

            if (delete == null)
            {
                return BadRequest("Item not found");
            }

            _context.Expenses.Remove(delete);
            await _context.SaveChangesAsync();

            return Ok($"Item with ID {Id} deleted successfully.");
            // Returns deleted object
        }


    }
}
