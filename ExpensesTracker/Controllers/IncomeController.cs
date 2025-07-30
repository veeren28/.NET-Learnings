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

        // GET: api/Income
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var list = await _context.Incomes
                .Where(i => i.UserId == userId)
                .ToListAsync();

            return Ok(list);
        }

        // POST: api/Income
        [HttpPost]
        public async Task<IActionResult> AddIncome([FromBody] AddIncomeDTO incomeDTO)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var user = await _context.Users.FindAsync(userId);
            if (user == null) return BadRequest("User not found");

            var now = incomeDTO.Date ?? DateTime.Now;

            // Update user balance
            user.Balance += incomeDTO.Amount;
             _context.Users.Update(user);

            // Create and add transaction
            var transaction = new TransactionModel
            {
                Title = incomeDTO.Title,
                Amount = incomeDTO.Amount,
                Description = incomeDTO.Description,
                Balance = user.Balance,
                Date = now,
                UserID = userId,
                Type = "Income",
                
            };
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync(); // Save transaction to get transaction.Id

            // Create and add income entry
            var income = new IncomeModel
            {
                Title = incomeDTO.Title,
                Description = incomeDTO.Description,
                Date = now,
                Amount = incomeDTO.Amount,
                Balance = user.Balance,
                UserId = userId,
                TransactionId = transaction.Id
            };
            await _context.Incomes.AddAsync(income);

            // Update user balance
            _context.Users.Update(user);

            await _context.SaveChangesAsync();

            return Ok("Income and Transaction added successfully.");
        }

        // PUT: api/Income/{id}
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchIncome(int id, [FromBody] UpdateIncomeDTO dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var user = await _context.Users.FindAsync(userId);
            if (user == null) return BadRequest("User not found");

            var income = await _context.Incomes.FindAsync(id);
            if (income == null || income.UserId != userId)
                return NotFound("Income not found or unauthorized.");

            var transaction = await _context.Transactions.FindAsync(income.TransactionId);
            if (transaction == null || transaction.UserID != userId)
                return NotFound("Transaction not found or unauthorized.");

            // Refund old amount if amount is changing
            if (dto.Amount.HasValue && dto.Amount.Value != income.Amount)
            {
                user.Balance -= income.Amount; // remove old
                user.Balance += dto.Amount.Value; // add new
                income.Amount = dto.Amount.Value;
                transaction.Amount = dto.Amount.Value;
                income.Balance = user.Balance;
                transaction.Balance = user.Balance;
            }

            // Patch other fields
            if (!string.IsNullOrWhiteSpace(dto.Title))
            {
                income.Title = dto.Title;
                transaction.Title = dto.Title;
            }

            if (!string.IsNullOrWhiteSpace(dto.Description))
            {
                income.Description = dto.Description;
                transaction.Description = dto.Description;
            }

            if (dto.Date.HasValue)
            {
                income.Date = dto.Date.Value;
                transaction.Date = dto.Date.Value;
            }

            income.UpdatedAt = DateTime.Now;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok("Income patched successfully.");
        }


        // DELETE: api/Income/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIncome(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var user = await _context.Users.FindAsync(userId);
            if (user == null) return BadRequest("User not found");

            var income = await _context.Incomes.FindAsync(id);
            if (income == null || income.UserId != userId)
                return NotFound("Income not found or unauthorized.");

            // Adjust balance
            user.Balance -= income.Amount;
            _context.Users.Update(user);

            // Remove income
            _context.Incomes.Remove(income);

            // Remove linked transaction if exists
            var transaction = await _context.Transactions.FindAsync(income.TransactionId);
            if (transaction != null)
                _context.Transactions.Remove(transaction);

            await _context.SaveChangesAsync();

            return Ok("Income and linked transaction deleted.");
        }
    }
}
