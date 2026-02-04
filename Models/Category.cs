using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ProductCatalogV2.Models
{
    [PrimaryKey("Id")]
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Category Name is required.")]
        [Display(Name = "Category Name")]
        public string? CategoryName { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}
