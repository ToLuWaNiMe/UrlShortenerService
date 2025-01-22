namespace UrlShortenerService.Services
{
    public interface IUrlService
    {
        Task<string> ShortenUrlAsync(string longUrl);
        //Task<string> GetOriginalUrlAsync(string shortUrl);
        Task<string> GetOriginalUrlWithCacheAsync(string shortUrl);
        Task<int> GetAccessCountAsync(string shortUrl);
    }
}
