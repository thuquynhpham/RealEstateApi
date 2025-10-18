using System.ComponentModel.DataAnnotations;

namespace RealEstate.Domain.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required(ErrorMessage ="Category Name can't be null or empty")]
        public required string Name { get; set; }
        [Required(ErrorMessage = "Category ImageUrl can't be null or empty")]
        public string? ImageUrl { get; set; }

        public ICollection<Property>? Properties { get; set; }
    }
}
