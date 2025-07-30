using ExpensesTracker.Data;
using ExpensesTracker.DTOs;
using ExpensesTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ExpensesTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransactionController : ControllerBase

    {
        private readonly AppContextDb _context;
        private readonly UserManager<UserApplication> _userManager;
        public TransactionController(AppContextDb Context, UserManager<UserApplication> userManager)
        {
            _context = Context;
            _userManager = userManager;

        }
        [HttpGet]
        public async Task<IActionResult> Get(
            string? categoryName,
            string? description,
            string? title,
            decimal? minAmount,
            decimal? maxAmount,
            DateTime? startDate,
            DateTime? endDate)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return BadRequest("No such user exists");

            var user = await _context.Users.FindAsync(userId);
            if (user == null) return NotFound("User ID not found");

            var query = _context.Transactions
                .Where(t => t.UserID == userId);

            if (!string.IsNullOrWhiteSpace(categoryName))
            {
                query = query.Where(t => t.CategoryName == categoryName);
            }


            if (minAmount.HasValue)
            {
                query = query.Where(t => t.Amount >= minAmount);
            }

            if (maxAmount.HasValue)
            {
                query = query.Where(t => t.Amount <= maxAmount);
            }

            if (startDate.HasValue)
            {
                query = query.Where(t => t.Date >= startDate);
            }

            if (endDate.HasValue)
            {
                query = query.Where(t => t.Date <= endDate);
            }

            if (!string.IsNullOrWhiteSpace(description))
            {
                query = query.Where(t => t.Description.Contains(description));
            }

            if (!string.IsNullOrWhiteSpace(title))
            {
                query = query.Where(t => t.Title.Contains(title));
            }

            var result = await query.Select(t => new TransactionDTO
            {
                Id = t.Id,
                Amount = t.Amount,
                Type = t.Type,
                Title = t.Title,
                Description = t.Description,
                Date = t.Date,
                Balance = t.Balance
            }).ToListAsync();

            return Ok(result);


            //[HttpGet]
            //public async Task<IActionResult> Get() {

            //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //       if (userId == null) return BadRequest("No such user exists");

            //       var user = await _context.Users.FindAsync(userId);
            //       if (user == null) return NotFound("User ID not found");

            //       var query = _context.Transactions
            //           .Where(t => t.UserID == userId);

            //    return Ok(query);
            //}
        }
    }
}
