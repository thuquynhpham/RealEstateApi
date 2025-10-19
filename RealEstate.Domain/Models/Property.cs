using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RealEstate.Domain.Models
{
    public class Property
    {
        [Key]
        public int PropertyId { get; set; }
        public required string Name { get; set; }
        public required string Detail { get; set; }
        public required string Address { get; set; }
        public string? ImageUrl { get; set; }
        public required decimal Price { get; set; }
        public bool IsTrending { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        [JsonIgnore]
        public int UserId { get; set; }
        [JsonIgnore]
        public User? User { get; set; }


    }
}
