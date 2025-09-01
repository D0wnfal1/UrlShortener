using Microsoft.EntityFrameworkCore;
using UrlShortener.Domain.Entities;

namespace UrlShortener.DataAccess.DataAccess;

public class ApplicationDbContext : DbContext
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
	{

	}

	public DbSet<ShortUrl> ShortUrls => Set<ShortUrl>();
	public DbSet<UrlVisit> UrlVisits => Set<UrlVisit>();

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<ShortUrl>()
			.HasIndex(su => su.ShortCode)
			.IsUnique();
	}
}