namespace AirWaterStore.API.DTOs
{
    public class CreateReviewDto
    {
        public int UserId { get; set; }      // The reviewer
        public int GameId { get; set; }      // The game being reviewed
        public int Rating { get; set; }      // Rating from 1 to 5
        public string? Comment { get; set; } // Optional comment
    }
}
