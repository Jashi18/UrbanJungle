using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrbanJungle.Application.Interfaces;
using UrbanJungle.Application.Models.PlantModels;
using UrbanJungle.Domain.Data;
using UrbanJungle.Domain.Entities;

namespace UrbanJungle.Application.Implementation
{
    public class PlantService : IPlantService
    {
        private readonly ApplicationDbContext _context;

        public PlantService(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public IEnumerable<PlantResponse> GetAllPlants()
        {
            var plants = _context.Plants
                .Include(p => p.Category)
                .Where(p => p.DeleteDate == null)
                .ToList();

            return plants.Select(p => new PlantResponse
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                StockQuantity = p.StockQuantity,
                PlantType = p.PlantType,
                CategoryId = p.CategoryId,
                CategoryName = p.Category?.Name ?? string.Empty
            }).ToList();
        }
        public PlantResponse GetPlantById(int id)
        {
            var plant = _context.Plants
                .Include(p => p.Category)
                .FirstOrDefault(p => p.Id == id && p.DeleteDate == null);

            if (plant == null)
            {
                throw new Exception("Plant not found");
            }

            return new PlantResponse
            {
                Id = plant.Id,
                Name = plant.Name,
                Description = plant.Description,
                Price = plant.Price,
                ImageUrl = plant.ImageUrl,
                StockQuantity = plant.StockQuantity,
                PlantType = plant.PlantType,
                CategoryId = plant.CategoryId,
                CategoryName = plant.Category?.Name ?? string.Empty
            };
        }
        public IEnumerable<PlantResponse> GetPlantsByCategory(int categoryId)
        {
            var plants = _context.Plants
                .Include(p => p.Category)
                .Where(p => p.CategoryId == categoryId && p.DeleteDate == null)
                .ToList();

            return plants.Select(p => new PlantResponse
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                StockQuantity = p.StockQuantity,
                PlantType = p.PlantType,
                CategoryId = p.CategoryId,
                CategoryName = p.Category?.Name ?? string.Empty
            }).ToList();
        }
        public PlantResponse CreatePlant(CreatePlantRequest request)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == request.CategoryId && c.DeleteDate == null);
            if (category == null)
            {
                throw new Exception("Category not found");
            }

            var plant = new Plant
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                ImageUrl = request.ImageUrl,
                StockQuantity = request.StockQuantity,
                PlantType = request.PlantType,
                CategoryId = request.CategoryId,
                CreateDate = DateTime.UtcNow
            };

            _context.Plants.Add(plant);
            _context.SaveChanges();

            return new PlantResponse
            {
                Id = plant.Id,
                Name = plant.Name,
                Description = plant.Description,
                Price = plant.Price,
                ImageUrl = plant.ImageUrl,
                StockQuantity = plant.StockQuantity,
                PlantType = plant.PlantType,
                CategoryId = plant.CategoryId,
                CategoryName = category.Name
            };
        }
        public PlantResponse UpdatePlant(UpdatePlantRequest request)
        {
            var plant = _context.Plants.FirstOrDefault(p => p.Id == request.Id && p.DeleteDate == null);
            if (plant == null)
            {
                throw new Exception("Plant not found");
            }

            string categoryName = string.Empty;
            if (request.CategoryId.HasValue)
            {
                var category = _context.Categories.FirstOrDefault(c => c.Id == request.CategoryId && c.DeleteDate == null);
                if (category == null)
                {
                    throw new Exception("Category not found");
                }
                categoryName = category.Name;
                plant.CategoryId = request.CategoryId.Value;
            }
            else
            {
                var category = _context.Categories.FirstOrDefault(c => c.Id == plant.CategoryId && c.DeleteDate == null);
                categoryName = category?.Name ?? string.Empty;
            }

            plant.Name = request.Name ?? plant.Name;
            plant.Description = request.Description ?? plant.Description;
            plant.Price = request.Price ?? plant.Price;
            plant.ImageUrl = request.ImageUrl ?? plant.ImageUrl;
            plant.StockQuantity = request.StockQuantity ?? plant.StockQuantity;
            plant.PlantType = request.PlantType ?? plant.PlantType;
            plant.UpdateDate = DateTime.UtcNow;

            _context.Plants.Update(plant);
            _context.SaveChanges();

            return new PlantResponse
            {
                Id = plant.Id,
                Name = plant.Name,
                Description = plant.Description,
                Price = plant.Price,
                ImageUrl = plant.ImageUrl,
                StockQuantity = plant.StockQuantity,
                PlantType = plant.PlantType,
                CategoryId = plant.CategoryId,
                CategoryName = categoryName
            };
        }
        public PlantResponse DeletePlant(int id)
        {
            var plant = _context.Plants
                .Include(p => p.Category)
                .FirstOrDefault(p => p.Id == id && p.DeleteDate == null);

            if (plant == null)
            {
                throw new Exception("Plant not found");
            }

            plant.DeleteDate = DateTime.UtcNow;
            _context.SaveChanges();

            return new PlantResponse
            {
                Id = plant.Id,
                Name = plant.Name,
                Description = plant.Description,
                Price = plant.Price,
                ImageUrl = plant.ImageUrl,
                StockQuantity = plant.StockQuantity,
                PlantType = plant.PlantType,
                CategoryId = plant.CategoryId,
                CategoryName = plant.Category?.Name ?? string.Empty
            };
        }
        public bool UpdatePlantStock(int id, int quantity)
        {
            var plant = _context.Plants.FirstOrDefault(p => p.Id == id && p.DeleteDate == null);
            if (plant == null)
            {
                throw new Exception("Plant not found");
            }

            if (quantity < 0 && plant.StockQuantity < Math.Abs(quantity))
            {
                return false;
            }

            plant.StockQuantity += quantity;
            plant.UpdateDate = DateTime.UtcNow;

            _context.Plants.Update(plant);
            _context.SaveChanges();

            return true;
        }
    }
}
