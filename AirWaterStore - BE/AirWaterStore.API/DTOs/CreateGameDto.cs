namespace AirWaterStore.API.DTOs
{
    public class CreateGameDto
    {
        public string? ThumbnailUrl { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string? Genre { get; set; }
        public string? Developer { get; set; }
        public string? Publisher { get; set; }
        public DateOnly? ReleaseDate { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
