using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISE309_SecondHandMarket.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }

        [NotMapped]
        [Display(Name = "Upload Image")]
        public IFormFile? ImageFile { get; set; }

        [Required(ErrorMessage = "Contact information is required")]
        public string? ContactInfo { get; set; }

        public string? UserId { get; set; }

        [Required(ErrorMessage = "Please select a category")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public Category? Category { get; set; }
    }
}