using Microsoft.AspNetCore.Mvc;
using UrbanJungle.Application.Interfaces;
using UrbanJungle.Application.Models.CategoryModels;

namespace UrbanJungle.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public IEnumerable<CategoryResponse> GetAllCategories()
        {
            return _categoryService.GetAllCategories();
        }

        [HttpGet("{id}")]
        public CategoryResponse GetCategoryById(int id)
        {
            return _categoryService.GetCategoryById(id);
        }

        [HttpPost]
        public CategoryResponse CreateCategory(CreateCategoryRequest request)
        {
            return _categoryService.CreateCategory(request);
        }

        [HttpPut]
        public CategoryResponse UpdateCategory(UpdateCategoryRequest request)
        {
            return _categoryService.UpdateCategory(request);
        }

        [HttpDelete("{id}")]
        public CategoryResponse DeleteCategory(int id)
        {
            return _categoryService.DeleteCategory(id);
        }
    }
}
