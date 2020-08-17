namespace SixBDigital.Domain.Entities.Abstract
{
	using System;

	public interface IEntity
	{
		public int Id { get; }
		public DateTime DateCreated { get; }
		public DateTime? DateEdited { get; }
	}
}
