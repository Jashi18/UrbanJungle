using UrbanJungle.Application.Interfaces;
using UrbanJungle.Application.Models.CategoryModels;
using UrbanJungle.Domain.Data;
using UrbanJungle.Domain.Entities;

namespace UrbanJungle.Application.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;
        public CategoryService(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }


        public IEnumerable<CategoryResponse> GetAllCategories()
        {
            var categories = _context.Categories.Where(c => c.DeleteDate == null);

            return categories.Select(c => new CategoryResponse
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();
        }
        public CategoryResponse GetCategoryById(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id && c.DeleteDate == null);
            if(category == null)
            {
                throw new Exception("Category not found");
            }

            return new CategoryResponse
            {
                Id = category.Id,
                Name = category.Name
            };
        }
        public CategoryResponse CreateCategory (CreateCategoryRequest request)
        {
            var category = new Category
            {
                Name = request.Name,
                Description = request.Description,
                ImageUrl = request.ImageUrl,
                CreateDate = DateTime.UtcNow
            };

            _context.Categories.Add(category);
            _context.SaveChanges();
            return new CategoryResponse
            {
                Id = category.Id,
                Name = category.Name
            };
        }
        public CategoryResponse UpdateCategory (UpdateCategoryRequest request)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == request.Id && c.DeleteDate == null);
            if (category == null)
            {
                throw new Exception("Category not found");
            }
            category.Name = request.Name ?? category.Name;
            category.Description = request.Description ?? category.Description;
            category.ImageUrl = request.ImageUrl ?? category.ImageUrl;
            category.UpdateDate = DateTime.UtcNow;
            _context.Categories.Update(category);
            _context.SaveChanges();
            return new CategoryResponse
            {
                Id = category.Id,
                Name = category.Name
            };
        }
        public CategoryResponse DeleteCategory (int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id && c.DeleteDate == null);
            if (category == null)
            {
                throw new Exception("Category not found");
            }
            category.DeleteDate = DateTime.UtcNow;
            _context.SaveChanges();
            return new CategoryResponse
            {
                Id = category.Id,
                Name = category.Name
            };
        }
    }
}
