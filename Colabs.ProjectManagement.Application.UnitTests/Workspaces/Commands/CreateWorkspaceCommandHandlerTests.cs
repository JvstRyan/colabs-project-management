using Colabs.ProjectManagement.Application.Contracts;
using Colabs.ProjectManagement.Application.Contracts.Persistence;
using Colabs.ProjectManagement.Application.Features.Workspaces.Commands.CreateWorkspace;
using Colabs.ProjectManagement.Application.Features.Workspaces.Queries.GetAllWorkspaces;
using Colabs.ProjectManagement.Domain.Entities.Workspaces;
using Moq;
using Shouldly;

namespace Colabs.ProjectManagement.Application.UnitTests.Workspaces.Mocks.Commands
{
    public class CreateWorkspaceCommandHandlerTests
    {
        private readonly Mock<IGenericRepository<Workspace>> _mockGenericWorkspaceRepository;
        private readonly Mock<ICurrentLoggedInUserService> _mockUserService;
        private readonly CreateWorkspaceCommandHandler _handler;
        private readonly string _testUserId = "test-user-id";

        public CreateWorkspaceCommandHandlerTests()
        {
            _mockGenericWorkspaceRepository = new Mock<IGenericRepository<Workspace>>();
            _mockUserService = new Mock<ICurrentLoggedInUserService>();
            _mockUserService.Setup(s => s.UserId).Returns(_testUserId); 
            
            _handler  = new CreateWorkspaceCommandHandler(
                _mockGenericWorkspaceRepository.Object,
                _mockUserService.Object);
        }

        [Fact]
        public async Task Handle_ValidCommand_ReturnSuccessResponse()
        {
            // Arange
            var command = WorkspaceTestMethods.CreateValidCommand();
            
            _mockGenericWorkspaceRepository.Setup(r => r.AddAsync(It
                .IsAny<Workspace>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
            
            // Act
            var result = await _handler.Handle(command, CancellationToken.None);
            
            // Assert
            result.ShouldNotBeNull();
            result.Success.ShouldBeTrue();
            result.Workspace.WorkspaceId.ShouldNotBeNullOrEmpty();
            
            _mockGenericWorkspaceRepository.Verify(
                r => r.AddAsync(
                    It.Is<Workspace>(w =>
                        w.Name == command.Name &&
                        w.Description == command.Description &&
                        w.OwnerId == _testUserId),
                    It.IsAny<CancellationToken>()),
                Times.Once);
            
        }

        [Fact]
        public async Task Handle_InvalidCommand_ReturnsValidationErrors()
        {
            // Arrange
            var command = WorkspaceTestMethods.CreateInvalidCommand();
            
            // Act
            var result = await _handler.Handle(command, CancellationToken.None);
            
            // Assert
            result.ShouldNotBeNull();
            result.Success.ShouldBeFalse();
            result.ValidationErrors.ShouldNotBeEmpty();
            
            _mockGenericWorkspaceRepository.Verify(r => r.AddAsync(It.IsAny<Workspace>(),
               It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
