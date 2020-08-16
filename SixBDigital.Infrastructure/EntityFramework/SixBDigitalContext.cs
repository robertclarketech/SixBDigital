namespace SixBDigital.Infrastructure.EntityFramework
{
	using Microsoft.EntityFrameworkCore;
	using SixBDigital.Domain.Entities;
	using BCrypt.Net;
	using System;

	public class SixBDigitalContext : DbContext
	{
		public SixBDigitalContext(DbContextOptions<SixBDigitalContext> options)
			: base(options)
		{
		}

		public DbSet<Booking>? Bookings { get; set; }
		public DbSet<User>? Users { get; set; }

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
				builder.Property(x => x.Approved).IsRequired();
			});

			modelBuilder.Entity<User>(builder =>
			{
				builder.Property(x => x.Id).IsRequired().Metadata.IsPrimaryKey();
				builder.Property(x => x.Username).IsRequired();
				builder.Property(x => x.Password).IsRequired();
				builder.HasData(new
				{
					Id = 1,
					Username = "admin",
					Password = BCrypt.EnhancedHashPassword("SHzMrX5QgHPhnGnl"),
					DateCreated = DateTime.Now
				});
			});
		}
	}
}
