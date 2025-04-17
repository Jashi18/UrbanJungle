namespace UrbanJungle.Domain.Entities
{
    public class Category : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;

        public ICollection<Plant> Plants { get; set; } = new List<Plant>();
    }
}
