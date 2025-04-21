using UrbanJungle.Application.Models.PlantModels;

namespace UrbanJungle.Application.Interfaces
{
    public interface IPlantService
    {
        IEnumerable<PlantResponse> GetAllPlants();
        PlantResponse GetPlantById(int id);
        IEnumerable<PlantResponse> GetPlantsByCategory(int categoryId);
        PlantResponse CreatePlant(CreatePlantRequest request);
        PlantResponse UpdatePlant(UpdatePlantRequest request);
        PlantResponse DeletePlant(int id);
        bool UpdatePlantStock(int id, int quantity);
    }
}
