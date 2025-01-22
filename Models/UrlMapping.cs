namespace UrlShortenerService.Models
{
    public class UrlMapping
    {
        public int Id { get; set; }
        public string LongUrl { get; set; }
        public string ShortUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int AccessCount { get; set; } = 0;
    }
}
