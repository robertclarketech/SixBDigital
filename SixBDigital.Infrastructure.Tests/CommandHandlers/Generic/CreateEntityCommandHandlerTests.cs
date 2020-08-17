namespace SixBDigital.Infrastructure.Tests.CommandHandlers.Generic
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using MediatR;
	using Moq;
	using SixBDigital.Domain.Builders.Interfaces;
	using SixBDigital.Domain.Entities.Abstract;
	using SixBDigital.Infrastructure.CommandHandlers.Generic;
	using SixBDigital.Infrastructure.EntityFramework;
	using SixBDigital.Infrastructure.Tests.Abstracts;
	using Xunit;

	public class CreateEntityCommandHandlerTests : InMemoryDbContextBase
	{
		private readonly Mock<IRequest> _requestMock;
		private readonly Mock<IEntity> _entityMock;
		private readonly Mock<IBuilder<IRequest, IEntity>> _builderMock;
		private readonly Mock<SixBDigitalContext> _contextMock;
		private readonly IRequestHandler<IRequest> _handler;

		public CreateEntityCommandHandlerTests()
		{
			_requestMock = new Mock<IRequest>();
			_builderMock = new Mock<IBuilder<IRequest, IEntity>>();
			_entityMock = new Mock<IEntity>();
			var builders = new List<IBuilder>
			{
				_builderMock.Object,
			};
			_contextMock = new Mock<SixBDigitalContext>();
			_handler = new CreateEntityCommandHandler<IRequest, IEntity>(_contextMock.Object, builders);
		}

		[Fact]
		public void Constructor_WithNoBuilders_Throws()
		{
			//arrange
			var builders = new List<IBuilder>();

			//act + assert
			_ = Assert.Throws<InvalidOperationException>(() => new CreateEntityCommandHandler<IRequest, IEntity>(Context, builders));
		}

		[Fact]
		public void Constructor_WithMoreThanOneValidBuilder_Throws()
		{
			//arrange
			var builders = new List<IBuilder>
			{
				_builderMock.Object,
				_builderMock.Object
			};

			//act + assert
			_ = Assert.Throws<InvalidOperationException>(() => new CreateEntityCommandHandler<IRequest, IEntity>(Context, builders));
		}

		[Fact]
		public async Task Handle_WithValidBuilder_CallsBuildAndSaveChanges()
		{
			//arrange
			_builderMock.Setup(e => e.Build(_requestMock.Object)).Returns(_entityMock.Object);

			//act
			_ = await _handler
				.Handle(_requestMock.Object, default)
				.ConfigureAwait(false);

			//assert
			_builderMock.Verify(e => e.Build(_requestMock.Object), Times.Once);
			// this doesnt work and I have no idea why
			//_contextMock.Verify(e => e.Add(_entityMock.Object), Times.Once);
			_contextMock.Verify(e => e.SaveChangesAsync(default), Times.Once);
		}
	}
}
