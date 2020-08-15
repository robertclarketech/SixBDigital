namespace SixBDigital.Infrastructure.EntityFramework
{
	using Microsoft.EntityFrameworkCore;
	using SixBDigital.Domain.Entities;

	public class SixBDigitalContext : DbContext
    {
		public SixBDigitalContext(DbContextOptions<SixBDigitalContext> options)
			: base(options)
		{
		}

		public DbSet<Booking>? Bookings { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Booking>(builder =>
			{
				builder.Property(x => x.Id).IsRequired().Metadata.IsPrimaryKey();
				builder.Property(x => x.BookingDate).IsRequired();
				builder.Property(x => x.Flexibility).IsRequired();
				builder.Property(x => x.VehicleSize).IsRequired();
				builder.Property(x => x.Name).IsRequired();
				builder.Property(x => x.ContactNumber).IsRequired();
				builder.Property(x => x.EmailAddress).IsRequired();
			});
		}
	}
}
