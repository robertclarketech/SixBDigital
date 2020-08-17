namespace SixBDigital.Domain.Entities.Abstract
{
	using System;

	public abstract class BaseEntity<T> : IEntity where T : BaseEntity<T>
	{
		public int Id { get; }

		public DateTime DateCreated { get; private set; } = DateTime.Now;
		public DateTime? DateEdited { get; private set; }

		internal T UpdateDateCreated(DateTime dateCreated)
		{
			DateCreated = dateCreated;
			return Validate();
		}

		internal T UpdateDateEdited(DateTime? dateEdited)
		{
			DateEdited = dateEdited;
			return Validate();
		}

		internal abstract T Validate(Action<T>? onFail = null);
	}
}
