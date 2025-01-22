using UrlShortenerService.Models;
using UrlShortenerService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace UrlShortenerService.Services
{
    public class UrlService : IUrlService
    {
        private readonly UrlShortenerDbContext _context;
        private readonly IMemoryCache _cache;

        public UrlService(UrlShortenerDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<string> ShortenUrlAsync(string longUrl)
        {
            // Check if the URL already exists
            var existing = await _context.UrlMappings.FirstOrDefaultAsync(u => u.LongUrl == longUrl);
            if (existing != null)
                return existing.ShortUrl;

            // Generate a unique short URL
            var shortUrl = GenerateShortUrl();
            while (await _context.UrlMappings.AnyAsync(u => u.ShortUrl == shortUrl))
            {
                shortUrl = GenerateShortUrl();
            }

            // Save to the database
            var urlMapping = new UrlMapping { LongUrl = longUrl, ShortUrl = shortUrl };
            _context.UrlMappings.Add(urlMapping);
            await _context.SaveChangesAsync();

            return shortUrl;
        }

        //public async Task<string> GetOriginalUrlAsync(string shortUrl)
        //{
        //    var mapping = await _context.UrlMappings.FirstOrDefaultAsync(u => u.ShortUrl == shortUrl);
        //    if (mapping == null)
        //        throw new Exception("URL not found");

        //    // Increment access count
        //    mapping.AccessCount++;
        //    await _context.SaveChangesAsync();

        //    return mapping.LongUrl;
        //}
        public async Task<string> GetOriginalUrlWithCacheAsync(string shortUrl)
        {
            if (_cache.TryGetValue(shortUrl, out string longUrl))
            {
                return longUrl;
            }

            var mapping = await _context.UrlMappings.FirstOrDefaultAsync(u => u.ShortUrl == shortUrl);
            if (mapping == null)
                throw new Exception("URL not found");

            // Cache the long URL for future requests
            _cache.Set(shortUrl, mapping.LongUrl, TimeSpan.FromMinutes(10));

            // Increment access count
            mapping.AccessCount++;
            await _context.SaveChangesAsync();

            return mapping.LongUrl;
        }

        public async Task<int> GetAccessCountAsync(string shortUrl)
        {
            var mapping = await _context.UrlMappings.FirstOrDefaultAsync(u => u.ShortUrl == shortUrl);
            if (mapping == null)
                throw new Exception("URL not found");

            return mapping.AccessCount;
        }


        private string GenerateShortUrl()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }

}
