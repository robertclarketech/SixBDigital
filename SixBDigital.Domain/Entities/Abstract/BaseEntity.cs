namespace SixBDigital.Domain.Entities.Abstract
{
	using System;
	using FluentValidation.Results;

	public abstract class BaseEntity
	{
		public int Id { get; }

		public DateTime DateCreated { get; private set; } = DateTime.Now;
		public DateTime? DateEdited { get; private set; }

		internal void SetDateCreated(DateTime dateCreated)
		{
			DateCreated = dateCreated;
		}

		internal void SetDateEdited(DateTime? dateEdited)
		{
			DateEdited = dateEdited;
		}

		public abstract ValidationResult Validate();
	}
}
