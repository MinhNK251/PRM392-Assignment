namespace AirWaterStore.API.DTOs
{
    public class UpdateUserDto
    {
        public int UserId { get; set; }
        public string Username { get; set; } = null!;
    }
}
