using ExpensesTracker.Data;
using ExpensesTracker.DTOs;
using ExpensesTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ExpensesTracker.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class IncomeController : ControllerBase
    {
        private readonly AppContextDb _context;
        private readonly UserManager<UserApplication> _userManager;

        public IncomeController(AppContextDb context, UserManager<UserApplication> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var list = await _context.Income
                .Where(i => i.UserId == userId)
                .ToListAsync();

            return Ok(list);
        }

        [HttpPost("AddIncome")]
        public async Task<IActionResult> AddIncome(AddIncomeDTO incomeDTO)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var user = await _context.Users.FindAsync(userId);
            if (user == null) return BadRequest("User not found");

            var category = await _context.Category
                .FirstOrDefaultAsync(e => e.CategoryName == incomeDTO.CategoryName);

            if (category == null)
                return NotFound("Category not found");

            var now = incomeDTO.Date ?? DateTime.Now;

            var income = new IncomeModel
            {
                Title = incomeDTO.Title,
                Description = incomeDTO.Description,
                Date = now,
                Amount = incomeDTO.Amount,
                CategoryId = category.Id,
                UserId = userId,
                CreatedAt = now
            };

            user.Balance += incomeDTO.Amount;

            await _context.Income.AddAsync(income);
            await _context.SaveChangesAsync();

            return Ok(income);
        }

        [HttpPut("EditIncome/{id}")]
        public async Task<IActionResult> UpdateIncome(int id, UpdateIncomeDTO incomeDTO)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var user = await _context.Users.FindAsync(userId);
            if (user == null) return BadRequest("User not found");

            var updateIncome = await _context.Income.FindAsync(id);
            if (updateIncome == null || updateIncome.UserId != userId)
                return BadRequest("Income not found or not authorized");

            var category = await _context.Category
                .FirstOrDefaultAsync(e => e.CategoryName == incomeDTO.CategoryName);

            if (category == null)
                return NotFound("Category not found");

            // Remove old amount, add new amount
            user.Balance -= updateIncome.Amount;
            user.Balance += incomeDTO.Amount;

            updateIncome.Title = incomeDTO.Title;
            updateIncome.Description = incomeDTO.Description;
            updateIncome.Date = incomeDTO.Date ?? updateIncome.Date;
            updateIncome.Amount = incomeDTO.Amount;
            updateIncome.CategoryId = category.Id;
            updateIncome.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return Ok(updateIncome);
        }

        [HttpDelete("DeleteIncome/{id}")]
        public async Task<IActionResult> DeleteIncome(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var user = await _context.Users.FindAsync(userId);
            if (user == null) return BadRequest("User not found");

            var income = await _context.Income.FindAsync(id);
            if (income == null || income.UserId != userId)
                return BadRequest("Income not found or not authorized");

            user.Balance -= income.Amount;
            _context.Income.Remove(income);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
} 