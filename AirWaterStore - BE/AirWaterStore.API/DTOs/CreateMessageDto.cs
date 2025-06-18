namespace AirWaterStore.API.DTOs
{
    public class CreateMessageDto
    {
        public int ChatRoomId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; } = null!;
    }
}
