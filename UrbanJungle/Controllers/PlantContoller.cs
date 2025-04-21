using Microsoft.AspNetCore.Mvc;
using UrbanJungle.Application.Interfaces;
using UrbanJungle.Application.Models.PlantModels;

namespace UrbanJungle.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlantController : ControllerBase
    {
        private readonly IPlantService _plantService;

        public PlantController(IPlantService plantService)
        {
            _plantService = plantService;
        }

        [HttpGet]
        public IEnumerable<PlantResponse> GetAllPlants()
        {
            return _plantService.GetAllPlants();
        }

        [HttpGet("{id}")]
        public PlantResponse GetPlantById(int id)
        {
            return _plantService.GetPlantById(id);
        }

        [HttpGet("category/{categoryId}")]
        public IEnumerable<PlantResponse> GetPlantsByCategory(int categoryId)
        {
            return _plantService.GetPlantsByCategory(categoryId);
        }

        [HttpPost]
        public PlantResponse CreatePlant(CreatePlantRequest request)
        {
            return _plantService.CreatePlant(request);
        }

        [HttpPut]
        public PlantResponse UpdatePlant(UpdatePlantRequest request)
        {
            return _plantService.UpdatePlant(request);
        }

        [HttpDelete("{id}")]
        public PlantResponse DeletePlant(int id)
        {
            return _plantService.DeletePlant(id);
        }

        [HttpPut("{id}/stock")]
        public IActionResult UpdateStock(int id, [FromBody] int quantity)
        {
            var success = _plantService.UpdatePlantStock(id, quantity);
            if (success)
            {
                return Ok(new { Message = "Stock updated successfully" });
            }
            else
            {
                return BadRequest(new { Message = "Insufficient stock" });
            }
        }
    }
}