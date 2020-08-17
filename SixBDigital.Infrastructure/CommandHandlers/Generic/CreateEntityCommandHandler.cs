namespace SixBDigital.Infrastructure.CommandHandlers.Generic
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading;
	using System.Threading.Tasks;
	using MediatR;
	using SixBDigital.Domain.Builders.Interfaces;
	using SixBDigital.Domain.Entities.Abstract;
	using SixBDigital.Infrastructure.EntityFramework;

	public class CreateEntityCommandHandler<TCommand, TEntity> : AsyncRequestHandler<TCommand>
		where TCommand : IRequest
		where TEntity : IEntity
	{
		private readonly SixBDigitalContext _context;
		private readonly IBuilder<TCommand, TEntity> _builder;

		public CreateEntityCommandHandler(SixBDigitalContext context, IEnumerable<IBuilder> builders)
		{
			_context = context;
			_builder = builders.OfType<IBuilder<TCommand, TEntity>>().Single();
		}

		protected override async Task Handle(TCommand request, CancellationToken cancellationToken)
		{
			var entity = _builder.Build(request);
			_ = _context.Add(entity);
			_ = await _context
				.SaveChangesAsync()
				.ConfigureAwait(false);
		}
	}
}
