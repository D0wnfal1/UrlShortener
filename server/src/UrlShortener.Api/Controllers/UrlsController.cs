using Microsoft.AspNetCore.Mvc;
using UrlShortener.Services.Services;

namespace UrlShortener.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UrlsController : ControllerBase
	{
		private readonly IUrlService _svc;
		public UrlsController(IUrlService svc) => _svc = svc;

		[HttpPost("shorten")]
		public async Task<IActionResult> Shorten([FromBody] string url) 
		{
			var code = await _svc.CreateShortUrlAsync(url);
			var full = $"{Request.Scheme}://{Request.Host}/{code}";
			return Ok(new { code, shortUrl = full });
		}

		[HttpGet("{code}")]
		public async Task<IActionResult> Resolve(string code)
		{
			var original = await _svc.PeekOriginalUrlAsync(code); 
			return Ok(new { code, originalUrl = original });
		}

		[HttpGet("{code}/stats")]
		public async Task<IActionResult> Stats(string code)
		{
			var s = await _svc.GetShortUrlStatisticsAsync(code);
			return Ok(new
			{
				s.ShortCode,
				s.OriginalUrl,
				totalVisits = s.Visits.Count,
				visits = s.Visits.Select(v => new { v.IpAddress, v.UserAgent, v.VisitedAt })
			});
		}
	}

}
