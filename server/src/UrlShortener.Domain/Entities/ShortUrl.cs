namespace UrlShortener.Domain.Entities;

public class ShortUrl
{
	public Guid Id { get; set; }
	public string OriginalUrl { get; set; }
	public string ShortCode { get; set; }
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

	public ICollection<UrlVisit> Visits { get; set; } = new List<UrlVisit>();
}