using BooksCollectionsApp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private readonly AppContextDb _context;

    public BookController(AppContextDb context)
    {
        _context = context;
    }
 
// GET: api/book or api/book?categoryId=1
[HttpGet]
    public IActionResult GetBooks([FromQuery] int? categoryId)
    {
        if (!ModelState.IsValid)
        {
            // Return the actual error so you can debug
            return BadRequest(ModelState);
        }
        var books = _context.Books
            .Include(b => b.Category)
            .Where(b => !categoryId.HasValue || b.CategoryId == categoryId)
            .Select(b => new
            {
                b.Id,
                b.Author,
                CategoryName = b.Category.Name
            })
            .ToList();

        return Ok(books);
    }
    [HttpPost("books")]
    public async Task<IActionResult> PutBooks([FromBody] BookModel book)
    {
        
      var added = await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return Ok(added.Entity);
        }
    
    [HttpPost("Add_Category")]
    public async Task<IActionResult> PutCategory([FromBody]CategoryModel categoryModel)
    {
        
        var cat =  await _context.Categories.AddAsync(categoryModel);   
        await _context.SaveChangesAsync();
        return Ok(cat.Entity);
    }
}
