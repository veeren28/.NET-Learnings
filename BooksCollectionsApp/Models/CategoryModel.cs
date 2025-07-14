using System.ComponentModel.DataAnnotations;

public class CategoryModel
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    // One-to-many relationship
    public List<BookModel> Books { get; set; }
}
