using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class BookModel
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }

    public string Author { get; set; }

    // Foreign Key
    public int CategoryId { get; set; }

    [ForeignKey("CategoryId")]
    public CategoryModel Category { get; set; }
}
