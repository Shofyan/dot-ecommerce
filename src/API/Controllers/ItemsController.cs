using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace API.Controllers
{
    public class ItemsController : Controller
    {
        private readonly IItemService _itemService;
        private readonly ICategoryService _categoryService;
        private const int PageSize = 10;

        public ItemsController(IItemService itemService, ICategoryService categoryService)
        {
            _itemService = itemService;
            _categoryService = categoryService;
        }

        // GET: Items
        public async Task<IActionResult> Index(int page = 1)
        {
            var allItems = (await _itemService.GetAllItemsAsync()).ToList();
            var totalCount = allItems.Count;
            var items = allItems.Skip((page - 1) * PageSize).Take(PageSize).ToList();

            var pagedResult = new PagedResult<ItemDto>
            {
                Items = items,
                PageNumber = page,
                PageSize = PageSize,
                TotalCount = totalCount
            };

            return View(pagedResult);
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var item = await _itemService.GetItemByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        // GET: Items/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CategoryId"] = new SelectList(await _categoryService.GetAllCategoriesAsync(), "Id", "Name");
            return View();
        }

        // POST: Items/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateItemDto itemDto)
        {
            if (ModelState.IsValid)
            {
                await _itemService.CreateItemAsync(itemDto);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(await _categoryService.GetAllCategoriesAsync(), "Id", "Name", itemDto.CategoryId);
            return View(itemDto);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var item = await _itemService.GetItemByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            var updateDto = new UpdateItemDto { Name = item.Name, Stock = item.Stock, Price = item.Price, CategoryId = item.CategoryId };
            ViewData["CategoryId"] = new SelectList(await _categoryService.GetAllCategoriesAsync(), "Id", "Name", item.CategoryId);
            ViewBag.ItemId = id;
            return View(updateDto);
        }

        // POST: Items/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UpdateItemDto itemDto)
        {
            if (ModelState.IsValid)
            {
                await _itemService.UpdateItemAsync(id, itemDto);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(await _categoryService.GetAllCategoriesAsync(), "Id", "Name", itemDto.CategoryId);
            return View(itemDto);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var item = await _itemService.GetItemByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _itemService.DeleteItemAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
