using ISE309_SecondHandMarket.Models;
using ISE309_SecondHandMarket.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ISE309_SecondHandMarket.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var myProducts = _productService.GetAll().Where(p => p.UserId == userId);
            return View(myProducts);
        }

        public IActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(_categoryService.GetAll(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            product.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ModelState.Remove("Category");
            ModelState.Remove("UserId");

            if (ModelState.IsValid)
            {
                if (product.ImageFile != null && product.ImageFile.Length > 0)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(product.ImageFile.FileName);
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await product.ImageFile.CopyToAsync(stream);
                    }
                    product.ImageUrl = "/images/" + fileName;
                }
                await _productService.AddAsync(product);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.CategoryId = new SelectList(_categoryService.GetAll(), "Id", "Name", product.CategoryId);
            return View(product);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = _productService.GetById(id);
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (product == null || product.UserId != currentUserId)
            {
                return Unauthorized();
            }

            ViewBag.CategoryId = new SelectList(_categoryService.GetAll(), "Id", "Name", product.CategoryId);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.Id) return NotFound();

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var existingProduct = _productService.GetByIdNoTracking(id);

            if (existingProduct == null || existingProduct.UserId != currentUserId) return Unauthorized();

            ModelState.Remove("Category");
            ModelState.Remove("UserId");

            if (ModelState.IsValid)
            {
                product.UserId = currentUserId;

                // Handle Image Upload using product.ImageFile from your View
                if (product.ImageFile != null && product.ImageFile.Length > 0)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(product.ImageFile.FileName);
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await product.ImageFile.CopyToAsync(stream);
                    }
                    product.ImageUrl = "/images/" + fileName;
                }
                else
                {
                    // Keep the existing image if no new one is uploaded
                    product.ImageUrl = existingProduct.ImageUrl;
                }

                await _productService.UpdateAsync(product);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.CategoryId = new SelectList(_categoryService.GetAll(), "Id", "Name", product.CategoryId);
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _productService.GetByIdNoTracking(id);
            if (product != null && product.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                _productService.Delete(id);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}