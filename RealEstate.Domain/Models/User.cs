using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RealEstate.Domain.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public required string Name { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public required string Email { get; set; }
        [Required]
        public required string Password { get; set; }
        public string? Phone { get; set; }

        public ICollection<Property>? Properties { get; set; }
    }
}
