using AnnouncementsAPI.Dtos;
using AnnouncementsAPI.Models;
using AnnouncementsAPI.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AnnouncementsAPI.Controllers
{
    [ApiController]
    [Route("Category")]
    public class CategoryController : ControllerBase
    {
        ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
        }


        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            IEnumerable<Category> categories = await _categoryService.GetAll();
            return Ok(categories);
        }

        /// <summary>
        /// Create a new category 
        /// </summary>
        /// <param name="category">Category to be added</param>
        /// <returns>BadRequest if the given category is null ,otherwise returns Ok</returns>
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody][Required] CategoryDto category)
        {
            if (category == null)
            {
                return BadRequest("Category cannot be null");
            }

            bool success = await _categoryService.Create(new Category
            {
                Id = Guid.NewGuid(),
                Name = category.Name
            });
            if (!success)
                return BadRequest("Invalid Category");
            return Ok();
        }

        /// <summary>
        /// Update an Exisetent category
        /// </summary>
        /// <param name="category">Category to update</param>
        /// <returns>BadRequest if the category is null or invalid, otherwise it returns Ok</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory([Required] Guid id, [FromBody] Category category)
        {
            if (category == null)
            {
                return BadRequest("Category cannot be null");
            }
            bool isOk = await _categoryService.Update(id, category);
            if (isOk)
                return BadRequest("Invalid Category");

            return Ok();
        }

        /// <summary>
        /// Delete an category by it's Id
        /// </summary>
        /// <param name="id">Category Id to be deleted</param>
        /// <returns>Ok if the category was successfully deleted, otherwise returns BadRequest</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            bool isOk = await _categoryService.Delete(id);
            if (isOk)
                return Ok();

            return NotFound("Category not found");
        }

        /// <summary>
        /// Get an category by it's Id
        /// </summary>
        /// <param name="id">Category Id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            var announcement = await _categoryService.Get(id);

            if (announcement == null)
            {
                return BadRequest("Category not found!");
            }
            return Ok(announcement);
        }
    }
}
