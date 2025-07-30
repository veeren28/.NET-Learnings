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
        public TransactionController( AppContextDb Context,UserManager<UserApplication> userManager) { 
            _context = Context;
            _userManager = userManager;
        
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return BadRequest("No Such User Exists");

            var user = await _context.Users.FindAsync(userId);
            if (user == null) return NotFound("UserId Not Found");

            var transactions = await _context.Transactions
                .Where(t => t.UserID == user.Id)
                .Select(t => new TransactionDTO
                {
                    Id = t.Id,
                    Amount = t.Amount,
                    Type = t.Type,
                    Title = t.Title,
                    Description = t.Description,
                    Date = t.Date,
                    Balance = t.Balance
                }).ToListAsync();

            return Ok(transactions);
        }
    }
}
