namespace UrbanJungle.Domain.Entities
{
    public class OrderItem : BaseEntity
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order? Order { get; set; }

        public int PlantId { get; set; }
        public Plant? Plant { get; set; }

        public string ProductName { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
