using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProductCatalogV2.Models
{
    [PrimaryKey("Id")]
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string? CategoryName { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}
