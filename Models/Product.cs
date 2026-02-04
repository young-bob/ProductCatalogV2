using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ProductCatalogV2.Models
{
    [PrimaryKey("Id")]
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string? ProductName { get; set; }
        [Required]
        public decimal ProductPrice { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }

        // Foreign Key
        public int CategoryId { get; set; }
        // Navigation property
        public Category? Category { get; set; }
    }
}
