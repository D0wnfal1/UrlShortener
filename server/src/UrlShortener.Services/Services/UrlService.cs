using Microsoft.EntityFrameworkCore;
using UrlShortener.DataAccess.DataAccess;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Services.Services;


public class UrlService : IUrlService
{
	private readonly ApplicationDbContext _context;

	public UrlService(ApplicationDbContext context)
	{
		_context = context;
	}

	public async Task<string> CreateShortUrlAsync(string originalUrl)
	{
		var shortCode = Guid.NewGuid().ToString("N")[..6];
		var shortUrl = new ShortUrl
		{
			Id = Guid.NewGuid(),
			OriginalUrl = originalUrl,
			ShortCode = shortCode
		};

		_context.ShortUrls.Add(shortUrl);
		await _context.SaveChangesAsync();
		return shortCode;
	}

	public async Task<string> GetOriginalUrlAsync(string shortCode, string ip, string userAgent)
	{
		var entity = await _context.ShortUrls
			.FirstOrDefaultAsync(x => x.ShortCode == shortCode);

		if (entity == null) throw new KeyNotFoundException("URL not found");

		_context.UrlVisits.Add(new UrlVisit
		{
			Id = Guid.NewGuid(),
			ShortUrlId = entity.Id,
			IpAddress = ip,
			UserAgent = userAgent
		});

		await _context.SaveChangesAsync();
		return entity.OriginalUrl;
	}

	public async Task<ShortUrl> GetShortUrlStatisticsAsync(string shortCode)
	{
		var shortUrl = await _context.ShortUrls
			.Include(u => u.Visits)
			.FirstOrDefaultAsync(x => x.ShortCode == shortCode);

		if (shortUrl == null)
		{
			throw new KeyNotFoundException("Short URL not found.");
		}

		return shortUrl;
	}

	public async Task<string> PeekOriginalUrlAsync(string shortCode)
	{
		var e = await _context.ShortUrls.FirstOrDefaultAsync(x => x.ShortCode == shortCode);
		if (e == null) throw new KeyNotFoundException("URL not found");
		return e.OriginalUrl;
	}
}

