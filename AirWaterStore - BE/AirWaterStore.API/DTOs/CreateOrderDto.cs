namespace AirWaterStore.API.DTOs
{
    public class CreateOrderDto
    {
        public int UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
    }
}
