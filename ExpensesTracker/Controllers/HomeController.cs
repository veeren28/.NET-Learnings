using ExpensesTracker.Data;
using ExpensesTracker.DTOs;
using ExpensesTracker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly AppContextDb _context;
        public HomeController(AppContextDb context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Expenses.ToList());
        }
        [HttpPost("AddItem")]
        public async Task<IActionResult> AddExpense(AddItemDTO itemDTO)
        {
            var item = new ExpensesModel
            {

                Title = itemDTO.Title,
                Description = itemDTO.Description,
                Date = DateTime.Now,
                Amount = itemDTO.Amount,
                Category = itemDTO.Category,

            };
            await _context.Expenses.AddAsync(item);
            await _context.SaveChangesAsync();
            return Ok(_context.Expenses.ToList());
        }
        [HttpPut("EditItem")]
        public async Task<IActionResult> UpdateItem(UpdateItemDTO itemDTO)
        {
            var UpdateItem = await _context.Expenses.FindAsync(itemDTO.Title);
            if (UpdateItem == null) { return BadRequest("Id not found"); }

            UpdateItem.Title = itemDTO.Title;
            UpdateItem.Description = itemDTO.Description;
            UpdateItem.Date = itemDTO.Date;
            UpdateItem.Amount = itemDTO.Amount;


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
