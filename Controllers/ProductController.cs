using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProductCatalogV2.BusinessLogic;
using ProductCatalogV2.Models;

namespace ProductCatalogV2.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductBL _productBL;
        private readonly CategoryBL _categoryBL;

        public ProductController(ProductBL productBL, CategoryBL categoryBL)
        {
            _productBL = productBL;
            _categoryBL = categoryBL;
        }

        // GET: Product
        public async Task<IActionResult> Index()
        {
            var products = await _productBL.GetAllProductsAsync();
            return View(products);
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productBL.GetProductByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Product/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CategoryId"] = new SelectList(await _categoryBL.GetAllCategoriesAsync(), "Id", "CategoryName");
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductName,ProductPrice,ImageUrl,Description,CategoryId")] Product product)
        {
            // Remove navigation property from validation
            ModelState.Remove("Category");

            if (ModelState.IsValid)
            {
                await _productBL.AddProductAsync(product);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(await _categoryBL.GetAllCategoriesAsync(), "Id", "CategoryName", product.CategoryId);
            return View(product);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productBL.GetProductByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(await _categoryBL.GetAllCategoriesAsync(), "Id", "CategoryName", product.CategoryId);
            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductName,ProductPrice,ImageUrl,Description,CategoryId")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            // Remove navigation property from validation
            ModelState.Remove("Category");

            if (ModelState.IsValid)
            {
                try
                {
                    await _productBL.UpdateProductAsync(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_productBL.ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(await _categoryBL.GetAllCategoriesAsync(), "Id", "CategoryName", product.CategoryId);
            return View(product);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productBL.GetProductByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productBL.DeleteProductAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
