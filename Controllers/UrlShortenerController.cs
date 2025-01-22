using Microsoft.AspNetCore.Mvc;
using UrlShortenerService.Services;

namespace UrlShortenerService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UrlShortenerController : ControllerBase
    {
        private readonly IUrlService _service;

        public UrlShortenerController(IUrlService service)
        {
            _service = service;
        }

        [HttpPost("shorten")]
        public async Task<IActionResult> ShortenUrl([FromBody] string longUrl)
        {
            if (string.IsNullOrWhiteSpace(longUrl))
                return BadRequest("Invalid URL");

            var shortUrl = await _service.ShortenUrlAsync(longUrl);
            return Ok(shortUrl);
        }

        [HttpGet("{shortUrl}")]
        public async Task<IActionResult> GetOriginalUrl(string shortUrl)
        {
            try
            {
                var longUrl = await _service.GetOriginalUrlWithCacheAsync(shortUrl);
                return Ok(new { longUrl });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("stats/{shortUrl}")]
        public async Task<IActionResult> GetUrlStats(string shortUrl)
        {
            try
            {
                var accessCount = await _service.GetAccessCountAsync(shortUrl);
                return Ok(new { ShortUrl = shortUrl, AccessCount = accessCount });
            }
            catch
            {
                return NotFound("URL not found");
            }
        }

    }

}
