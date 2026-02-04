using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductCatalogV2.Models
{
    [PrimaryKey("Id")]
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Product Name is required.")]
        [Display(Name = "Product Name")]
        public string? ProductName { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Price")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal ProductPrice { get; set; }

        [Display(Name = "Image")]
        public string? ImageUrl { get; set; }

        [Display(Name = "Description")]
        public string? Description { get; set; }


        [Display(Name = "Category")]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        [Display(Name = "Category Info")]
        public Category? Category { get; set; }
    }
}
