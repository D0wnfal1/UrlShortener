using Microsoft.AspNetCore.Mvc;
using UrlShortener.Services.Services;

namespace UrlShortener.Api.Controllers
{
	[ApiExplorerSettings(IgnoreApi = true)]
	[Route("")]
	public class RedirectController : ControllerBase
	{
		private readonly IUrlService _svc;
		public RedirectController(IUrlService svc) => _svc = svc;

		[HttpGet("{code}")]
		public async Task<IActionResult> Go(string code)
		{
			var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
			var ua = Request.Headers.UserAgent.ToString();

			var original = await _svc.GetOriginalUrlAsync(code, ip ?? "", ua);
			return Redirect(original); 
		}
	}
}
