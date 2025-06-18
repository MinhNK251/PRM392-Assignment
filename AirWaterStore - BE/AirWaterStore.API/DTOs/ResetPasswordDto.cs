namespace AirWaterStore.API.DTOs
{
    public class ResetPasswordDto
    {
        public int UserId { get; set; }
        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
    }
}
