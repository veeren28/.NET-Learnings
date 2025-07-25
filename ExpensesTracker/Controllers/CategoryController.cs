using ExpensesTracker.Data;
using ExpensesTracker.DTOs;

using ExpensesTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpensesTracker.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
      
        private readonly AppContextDb _context; 
        public CategoryController(AppContextDb context) { 
        _context = context; 
     
        }
        [HttpGet]
        public IActionResult Get() { 
        return Ok(_context.Category.ToList());
        }
        [HttpPost("AddCategory")]
        public async Task<IActionResult> AddCategory([FromBody]AddCategoryDTO CategoryDTO)
        {
            if (CategoryDTO == null || String.IsNullOrWhiteSpace(CategoryDTO.CategoryName)|| CategoryDTO.CategoryName.Contains(" ") ){ return BadRequest("Invalid"); }
            var categoryName = CategoryDTO.CategoryName;
            var check = await _context.Category.FirstOrDefaultAsync(c=>c.CategoryName==categoryName);
            if (check != null) { return BadRequest("Already Exists"); }
            var newCategory = new CategoryModel {
                
                CategoryName=categoryName };
            await _context.Category.AddAsync(newCategory);
            await _context.SaveChangesAsync();
            return Ok(newCategory.CategoryName +" Added sucessfully");


            

        }
        [HttpPut("EditCategory/{NameCategory}")]
        public async Task<IActionResult> EditCategory(string NameCategory,[FromBody]EditCategoryDTO editCategoryDTO)
        {
            if (!ModelState.IsValid) { return BadRequest(); }
          
            if (String.IsNullOrWhiteSpace(NameCategory) || NameCategory.Contains(" ")) return BadRequest("Invalid");
            if (editCategoryDTO == null || String.IsNullOrWhiteSpace(editCategoryDTO.CategoryName)|| editCategoryDTO.CategoryName.Contains(" ")) { return BadRequest("Invalid"); }
            var check  =  await _context.Category.FirstOrDefaultAsync(c=>c.CategoryName== NameCategory);
            if (check == null) { return NotFound("no such Entry Exists"); }
            check.CategoryName = editCategoryDTO.CategoryName;
            await _context.SaveChangesAsync();
            return Ok(check.CategoryName + " Added Sucesfully");

        }
        [HttpDelete("DeleteCategory/{NameOfCategory}")]
        public async Task<IActionResult> DeleteCategory(string NameOfCategory)
        {
            if(!ModelState.IsValid) { return BadRequest(); }
            if (String.IsNullOrWhiteSpace(NameOfCategory)||NameOfCategory.Contains(" ")) return BadRequest("Invalid");
            var delete = await _context.Category.FirstOrDefaultAsync(c=>c.CategoryName== NameOfCategory);
            if (delete == null) {   return NotFound("No such category Exists"); }
             _context.Category.Remove(delete);
            await _context.SaveChangesAsync();
            return Ok(delete.CategoryName + " deleted Sucessfully");
        }


    }
}
