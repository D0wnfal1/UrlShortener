namespace UrlShortener.Domain.Entities
{
	public class UrlVisit
	{
		public Guid Id { get; set; }
		public Guid ShortUrlId { get; set; }
		public ShortUrl ShortUrl { get; set; }
		public string IpAddress { get; set; }
		public string UserAgent { get; set; }
		public DateTime VisitedAt { get; set; } = DateTime.UtcNow;
	}
}
