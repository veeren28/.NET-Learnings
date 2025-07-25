using ExpensesTracker.Data;
using ExpensesTracker.DTOs;
using ExpensesTracker.Models;
using Microsoft.AspNetCore.Authorization;
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
        private readonly UserManager<UserApplication> _userManager;

        public HomeController(AppContextDb context, UserManager<UserApplication> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var list = await _context.Expenses
                .Where(e => e.UserId == userId)
                .ToListAsync();

            return Ok(list);
        }

        [HttpPost("AddItem")]
        public async Task<IActionResult> AddExpense(AddItemDTO itemDTO)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var user = await _context.Users.FindAsync(userId);
            if (user == null) return BadRequest("User not found");

            if (user.Balance < itemDTO.Amount)
                return BadRequest("Insufficient balance");

            var category = await _context.Category
                .FirstOrDefaultAsync(e => e.CategoryName == itemDTO.CategoryName);

            if (category == null)
                return NotFound("Category not found");

            user.Balance -= itemDTO.Amount;

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
        public async Task<IActionResult> UpdateItem(int id, UpdateItemDTO itemDTO)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var user = await _context.Users.FindAsync(userId);
            if (user == null) return BadRequest("User not found");

            var updateItem = await _context.Expenses.FindAsync(id);
            if (updateItem == null || updateItem.UserId != userId)
                return BadRequest("Expense not found or not authorized");

            var category = await _context.Category
                .FirstOrDefaultAsync(e => e.CategoryName == itemDTO.CategoryName);

            if (category == null)
                return NotFound("Category not found");

            // Refund old amount
            user.Balance += updateItem.Amount;

            // Check if enough balance to deduct new amount
            if (user.Balance < itemDTO.Amount)
                return BadRequest("Insufficient balance for update");

            user.Balance -= itemDTO.Amount;

            updateItem.Title = itemDTO.Title;
            updateItem.Description = itemDTO.Description;
            updateItem.Amount = itemDTO.Amount;
            updateItem.CategoryId = category.Id;
            updateItem.Date = itemDTO.Date;
            updateItem.UpdatedAt = DateTime.Now;
            updateItem.BalanceAfter = user.Balance;

            await _context.SaveChangesAsync();
            return Ok(updateItem);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var user = await _context.Users.FindAsync(userId);
            if (user == null) return BadRequest("User not found");

            var deleteItem = await _context.Expenses.FindAsync(id);
            if (deleteItem == null || deleteItem.UserId != userId)
                return BadRequest("Item not found or not authorized");

            user.Balance += deleteItem.Amount;

            _context.Expenses.Remove(deleteItem);
            await _context.SaveChangesAsync();

            return Ok($"Item '{deleteItem.Title}' with ID {id} deleted and balance updated.");
        }
    }
}
