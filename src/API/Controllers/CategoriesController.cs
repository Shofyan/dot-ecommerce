using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;
        private const int PageSize = 10;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: Categories
        public async Task<IActionResult> Index(int page = 1)
        {
            var allCategories = (await _categoryService.GetAllCategoriesAsync()).ToList();
            var totalCount = allCategories.Count;
            var categories = allCategories.Skip((page - 1) * PageSize).Take(PageSize).ToList();

            var pagedResult = new PagedResult<CategoryDto>
            {
                Items = categories,
                PageNumber = page,
                PageSize = PageSize,
                TotalCount = totalCount
            };

            return View(pagedResult);
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCategoryDto categoryDto)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.CreateCategoryAsync(categoryDto);
                return RedirectToAction(nameof(Index));
            }
            return View(categoryDto);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            var updateDto = new UpdateCategoryDto { Name = category.Name, Description = category.Description };
            ViewBag.CategoryId = id;
            return View(updateDto);
        }

        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UpdateCategoryDto categoryDto)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.UpdateCategoryAsync(id, categoryDto);
                return RedirectToAction(nameof(Index));
            }
            return View(categoryDto);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
