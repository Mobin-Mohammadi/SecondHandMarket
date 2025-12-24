using System.ComponentModel.DataAnnotations;

namespace ISE309_SecondHandMarket.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Category name is required.")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        [Display(Name = "Category Name")]
        public string Name { get; set; }

        [StringLength(200, ErrorMessage = "Description cannot exceed 200 characters.")]
        public string? Description { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}