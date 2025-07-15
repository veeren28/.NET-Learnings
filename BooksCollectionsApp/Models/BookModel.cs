using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class BookModel
{
    public int Id { get; set; }

    [Required(ErrorMessage ="Title is required")]
    public string Title { get; set; }
    [StringLength(20,ErrorMessage = "Name Should be Less than 20")]
    public string Author { get; set; }

    // Foreign Key
   
    public int CategoryId { get; set; }

    [ForeignKey("CategoryId")]
    public CategoryModel Category { get; set; }
}
