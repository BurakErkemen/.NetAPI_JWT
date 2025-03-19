namespace CoreLayer.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public Decimal Price  { get; set; }
        public int Stock { get; set; }
        public string? UserId { get; set; } 
    }
}
