using UrbanJungle.Domain.Enums;

namespace UrbanJungle.Domain.Entities
{
    public class Plant : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public int StockQuantity { get; set; }
        public PlantType PlantType { get; set; }
    }
}
