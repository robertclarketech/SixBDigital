namespace SixBDigital.Domain.Entities.Abstract
{
	using System;
	using FluentValidation;
	using FluentValidation.Results;

	public abstract class BaseEntity
	{
		public int Id { get; }

		public DateTime DateCreated { get; private set; } = DateTime.Now;
		public DateTime? DateEdited { get; private set; }

		internal void UpdateDateCreated(DateTime dateCreated)
		{
			DateCreated = dateCreated;
			var validationResults = Validate();
			if (!Validate().IsValid)
			{
				throw new ValidationException($"{GetType().Name} is not valid", validationResults.Errors);
			}
		}

		internal void UpdateDateEdited(DateTime? dateEdited)
		{
			DateEdited = dateEdited;
			var validationResults = Validate();
			if (!Validate().IsValid)
			{
				throw new ValidationException($"{GetType().Name} is not valid", validationResults.Errors);
			}
		}

		public abstract ValidationResult Validate();
	}
}
