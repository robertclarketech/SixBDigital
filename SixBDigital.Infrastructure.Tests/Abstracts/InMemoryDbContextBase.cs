namespace SixBDigital.Infrastructure.Tests.Abstracts
{
	using System;
	using System.Data.Common;
	using Microsoft.Data.Sqlite;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Infrastructure;
	using SixBDigital.Infrastructure.EntityFramework;

	public abstract class InMemoryDbContextBase : IDisposable
	{
		private readonly DbConnection _connection;
		public SixBDigitalContext Context { get; }

		protected InMemoryDbContextBase(DbContextOptions<SixBDigitalContext> options = null)
		{
			if (options == null)
			{
				options = new DbContextOptionsBuilder<SixBDigitalContext>()
					.UseSqlite(CreateInMemoryDatabase()).Options;
			}
			_connection = RelationalOptionsExtension.Extract(options).Connection;
			Context = new SixBDigitalContext(options);
			Context.Database.EnsureDeleted();
			Context.Database.EnsureCreated();
		}

		private static DbConnection CreateInMemoryDatabase()
		{
			var connection = new SqliteConnection("Filename=:memory:");

			connection.Open();

			return connection;
		}

		public void Dispose() => _connection.Dispose();
	}
}
