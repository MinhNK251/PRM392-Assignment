namespace AirWaterStore.API.DTOs
{
    public class CreateOrderDetailDto
    {
        public int OrderId { get; set; }
        public int GameId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
