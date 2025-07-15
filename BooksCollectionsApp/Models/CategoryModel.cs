using System.ComponentModel.DataAnnotations;

public class CategoryModel
{
    public int Id { get; set; }

    [Required(ErrorMessage ="Name is required")]
    [StringLength(20,ErrorMessage ="Name Length < 20")]
    public string Name { get; set; }

    // One-to-many relationship
    public List<BookModel> Books { get; set; }
}
