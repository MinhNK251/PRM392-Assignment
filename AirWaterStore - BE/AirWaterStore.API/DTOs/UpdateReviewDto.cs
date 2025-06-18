namespace AirWaterStore.API.DTOs
{
    public class UpdateReviewDto
    {
        public int UserId { get; set; }
        public int ReviewId { get; set; }    // Required for verification
        public int Rating { get; set; }      // Updated rating
        public string? Comment { get; set; } // Updated comment
    }
}
