using ISE309_SecondHandMarket.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ISE309_SecondHandMarket.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public HomeController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public IActionResult Index(int? categoryId)
        {
            // 1. Fetch all products
            var products = _productService.GetAll();

            // 2. Logic: If a category is selected, filter the list
            if (categoryId.HasValue && categoryId > 0)
            {
                products = products.Where(p => p.CategoryId == categoryId.Value).ToList();
            }

            // 3. Send Categories to the View so the "Pills" can be drawn
            ViewBag.Categories = _categoryService.GetAll();

            // 4. Send the SelectedCategory back so the "Active" pill stays highlighted
            ViewBag.SelectedCategory = categoryId;

            return View(products);
        }
    }
}