namespace UrbanJungle.Domain.Entities
{
    public class Order : BaseEntity
    {
        public int Id { get; set; }
        public Guid OrderNumber { get; set; }
        public string Status { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public string ShippingAddress { get; set; } = string.Empty;
        public string ShippingCity { get; set; } = string.Empty;

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
