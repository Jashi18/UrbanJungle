using UrbanJungle.Application.Models.CategoryModels;

namespace UrbanJungle.Application.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<CategoryResponse> GetAllCategories();
        CategoryResponse GetCategoryById(int id);
        CategoryResponse CreateCategory(CreateCategoryRequest request);
        CategoryResponse UpdateCategory(UpdateCategoryRequest request);
        CategoryResponse DeleteCategory(int id);
    }
}
