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
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIncome(int id, [FromBody] UpdateIncomeDTO incomeDTO)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var user = await _context.Users.FindAsync(userId);
            if (user == null) return BadRequest("User not found");

            var income = await _context.Incomes.FindAsync(id);
            if (income == null || income.UserId != userId)
                return NotFound("Income not found or unauthorized.");

            // Adjust user balance
            user.Balance = user.Balance - income.Amount + incomeDTO.Amount;
            _context.Users.Update(user);

            // Update income
            income.Title = incomeDTO.Title;
            income.Description = incomeDTO.Description;
            income.Date = incomeDTO.Date ?? income.Date;
            income.Amount = incomeDTO.Amount;
            income.Balance = user.Balance;

            // Update transaction
            var transaction = await _context.Transactions.FindAsync(income.TransactionId);
            if (transaction == null || transaction.UserID != userId)
                return BadRequest("Related transaction not found or unauthorized.");

            transaction.Title = incomeDTO.Title;
            transaction.Description = incomeDTO.Description;
            transaction.Date = incomeDTO.Date ?? income.Date;
            transaction.Amount = incomeDTO.Amount;
            transaction.Balance = user.Balance;

            // Save updates
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok("Income and Transaction updated successfully.");
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
