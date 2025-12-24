using ISE309_SecondHandMarket.Models;
using ISE309_SecondHandMarket.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ISE309_SecondHandMarket.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            var categories = _categoryService.GetAll();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (!ModelState.IsValid)
                return View(category);

            _categoryService.Add(category);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var category = _categoryService.GetById(id);
            if (category == null) return NotFound();
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (!ModelState.IsValid)
                return View(category);

            _categoryService.Update(category);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            _categoryService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
