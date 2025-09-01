using UrlShortener.Domain.Entities;

namespace UrlShortener.Services.Services;

public interface IUrlService
{
	Task<string> CreateShortUrlAsync(string originalUrl);
	Task<string> GetOriginalUrlAsync(string shortCode, string ip, string userAgent);
	Task<ShortUrl> GetShortUrlStatisticsAsync(string shortCode);
	public Task<string> PeekOriginalUrlAsync(string shortCode);
}