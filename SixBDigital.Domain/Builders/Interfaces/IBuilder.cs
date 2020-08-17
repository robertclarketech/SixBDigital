namespace SixBDigital.Domain.Builders.Interfaces
{
	using MediatR;
	using SixBDigital.Domain.Entities.Abstract;

	public interface IBuilder { }

	public interface IBuilder<TCommand, TEntity> : IBuilder
		where TCommand : IRequest
		where TEntity : IEntity
	{
		public TEntity Build(TCommand command);
	}
}
