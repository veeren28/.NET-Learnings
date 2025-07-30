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
            try{
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
                Type = "Expense",
                CategoryName = itemDTO.CategoryName,
                CategoryId = category.Id






            };
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();

            var item = new ExpensesModel
            {
                Title = itemDTO.Title,
                Description = itemDTO.Description,
                Date = DateTime.Now,
                Amount = itemDTO.Amount,
                CategoryId = category.Id,
                CategoryName = category.CategoryName,
                UserId = userId,
                BalanceAfter = user.Balance,
                TransactionId = transaction.Id,
                UserName = user.UserName
                
            };

            await _context.Expenses.AddAsync(item);
            await _context.SaveChangesAsync();

                return Ok(item.Title +" Added Succesfully"); }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

      [HttpPatch("{id}")]
public async Task<IActionResult> PatchItem(int id, UpdateItemDTO itemDTO)
{
    try
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var user = await _context.Users.FindAsync(userId);
        if (user == null) return BadRequest("User not found");

        var updateItem = await _context.Expenses
            .Include(e => e.Transaction)
            .FirstOrDefaultAsync(e => e.ExpenseId == id && e.UserId == userId);

        if (updateItem == null) return BadRequest("Expense not found or not authorized");

        // Adjust balance only if amount is being changed
        if (itemDTO.Amount.HasValue && itemDTO.Amount.Value != updateItem.Amount)
        {
            user.Balance += updateItem.Amount; // refund old
            if (user.Balance < itemDTO.Amount.Value)
                return BadRequest("Insufficient balance");

            user.Balance -= itemDTO.Amount.Value;
            updateItem.BalanceAfter = user.Balance;
            updateItem.Amount = itemDTO.Amount.Value;

            if (updateItem.Transaction != null)
            {
                updateItem.Transaction.Amount = itemDTO.Amount.Value;
                updateItem.Transaction.Balance = user.Balance;
            }

            _context.Users.Update(user);
        }

        if (!string.IsNullOrWhiteSpace(itemDTO.Title))
        {
            updateItem.Title = itemDTO.Title;
            if (updateItem.Transaction != null)
                updateItem.Transaction.Title = itemDTO.Title;
        }

        if (!string.IsNullOrWhiteSpace(itemDTO.Description))
        {
            updateItem.Description = itemDTO.Description;
            if (updateItem.Transaction != null)
                updateItem.Transaction.Description = itemDTO.Description;
        }

        if (itemDTO.Date.HasValue)
        {
            updateItem.Date = itemDTO.Date.Value;
            if (updateItem.Transaction != null)
                updateItem.Transaction.Date = itemDTO.Date.Value;
                }

                if (!string.IsNullOrWhiteSpace(itemDTO.CategoryName))
                {
                    var category = await _context.Category
                        .FirstOrDefaultAsync(c => c.CategoryName == itemDTO.CategoryName);
                    if (category == null) return NotFound("Category not found");

                    updateItem.CategoryId = category.Id;
                    updateItem.CategoryName = category.CategoryName;


                }

                updateItem.UpdatedAt = DateTime.Now;

        await _context.SaveChangesAsync();
        return Ok("Expense updated successfully.");
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"Internal error: {ex.Message}");
    }
}


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId == null) return Unauthorized();

                var user = await _context.Users.FindAsync(userId);
                if (user == null) return BadRequest("User not found");

                var deleteItem = await _context.Expenses
                    .Include(e => e.Transaction)
                    .FirstOrDefaultAsync(e => e.ExpenseId == id);

                if (deleteItem == null || deleteItem.UserId != userId)
                    return BadRequest("Item not found or not authorized");

                user.Balance += deleteItem.Amount;
                _context.Users.Update(user);

                //if (deleteItem.Transaction != null)
                //    _context.Transactions.Remove(deleteItem.Transaction);

                _context.Expenses.Remove(deleteItem);
                await _context.SaveChangesAsync();

                return Ok($"Item '{deleteItem.Title}' with ID {id} deleted and balance updated.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }





    }
}
