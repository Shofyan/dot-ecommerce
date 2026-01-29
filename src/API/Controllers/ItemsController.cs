using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemsController : ControllerBase
{
    private readonly IItemService _itemService;

    public ItemsController(IItemService itemService)
    {
        _itemService = itemService;
    }

    /// <summary>
    /// Get all items
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ItemDto>>> GetAll()
    {
        var items = await _itemService.GetAllItemsAsync();
        return Ok(items);
    }

    /// <summary>
    /// Get item by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<ItemDto>> GetById(Guid id)
    {
        var item = await _itemService.GetItemByIdAsync(id);
        if (item == null)
            return NotFound(new { message = $"Item with ID {id} not found." });

        return Ok(item);
    }

    /// <summary>
    /// Get items by category ID
    /// </summary>
    [HttpGet("category/{categoryId}")]
    public async Task<ActionResult<IEnumerable<ItemDto>>> GetByCategory(Guid categoryId)
    {
        var items = await _itemService.GetItemsByCategoryAsync(categoryId);
        return Ok(items);
    }

    /// <summary>
    /// Create a new item
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<ItemDto>> Create([FromBody] CreateItemDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var item = await _itemService.CreateItemAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Update an existing item
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<ItemDto>> Update(Guid id, [FromBody] UpdateItemDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var item = await _itemService.UpdateItemAsync(id, dto);
            if (item == null)
                return NotFound(new { message = $"Item with ID {id} not found." });

            return Ok(item);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Delete an item
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var result = await _itemService.DeleteItemAsync(id);
        if (!result)
            return NotFound(new { message = $"Item with ID {id} not found." });

        return NoContent();
    }
}
