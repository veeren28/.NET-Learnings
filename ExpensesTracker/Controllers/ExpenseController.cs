using ExpensesTracker.Data;
using ExpensesTracker.DTOs;
using ExpensesTracker.DTOs.SummaryDTO_s;
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
    public class ExpenseController : ControllerBase
    {
        private readonly AppContextDb _context;
        private readonly UserManager<UserApplication> _userManager;

        public ExpenseController(AppContextDb context, UserManager<UserApplication> userManager)
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

        //add expense.
        [HttpPost]
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
            _context.Users.Update(user);

            var transaction = new TransactionModel
            {
                Amount = itemDTO.Amount,
                Title = itemDTO.Title,
                Date = DateTime.Now,
                Description = itemDTO.Description,
                Balance = user.Balance,
                UserID = userId,





            };
            await _context.Transaction.AddAsync(transaction);
            await _context.SaveChangesAsync();

            var item = new ExpensesModel
            {
                Title = itemDTO.Title,
                Description = itemDTO.Description,
                Date = DateTime.Now,
                Amount = itemDTO.Amount,
                CategoryId = category.Id,
                UserId = userId,
                BalanceAfter = user.Balance,
                TransactionId = transaction.Id
            };

            await _context.Expenses.AddAsync(item);
            await _context.SaveChangesAsync();

            return Ok(item);
        }

        [HttpPut("{id}")]
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
            {
                _context.Users.Update(user);
                return BadRequest("Insufficient balance for update");
            }

            user.Balance -= itemDTO.Amount;


          
            _context.Users.Update(user);



            updateItem.Title = itemDTO.Title;
            updateItem.Description = itemDTO.Description;
            updateItem.Amount = itemDTO.Amount;
            updateItem.CategoryId = category.Id;
            updateItem.Date = itemDTO.Date;
            updateItem.UpdatedAt = DateTime.Now;
            updateItem.BalanceAfter = user.Balance;



            var updatetransactions = await _context.Transaction.FindAsync(updateItem.TransactionId);
            if (updatetransactions == null) return BadRequest();
            updatetransactions.Title = itemDTO.Title;
            updatetransactions.Description = itemDTO.Description;
            updatetransactions.Amount = itemDTO.Amount;
            updatetransactions.Date = itemDTO.Date;
            updatetransactions.Balance = user.Balance;


            await _context.SaveChangesAsync();
            return Ok(updateItem);
        }

        [HttpDelete("{id}")]
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
            _context.Users.Update(user);


            var deleteTransaction = await _context.Transaction.FindAsync(deleteItem.TransactionId);
            if (deleteTransaction == null) return BadRequest();
            _context.Transaction.Remove(deleteTransaction);
           

            _context.Expenses.Remove(deleteItem);
            await _context.SaveChangesAsync();

            return Ok($"Item '{deleteItem.Title}' with ID {id} deleted and balance updated.");
        }

    


    }
}
