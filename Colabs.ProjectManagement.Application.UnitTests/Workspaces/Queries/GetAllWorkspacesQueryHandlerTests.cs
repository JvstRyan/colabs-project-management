using Colabs.ProjectManagement.Application.Contracts;
using Colabs.ProjectManagement.Application.Contracts.Persistence;
using Colabs.ProjectManagement.Application.Features.Workspaces.Queries.GetAllWorkspaces;
using Colabs.ProjectManagement.Domain.Entities.Workspaces;
using Moq;
using Shouldly;

namespace Colabs.ProjectManagement.Application.UnitTests.Workspaces.Mocks.Queries
{
    public class GetAllWorkspacesQueryHandlerTests
    {
        private readonly Mock<IWorkspaceRepository> _mockWorkspaceRepository;
        private readonly Mock<ICurrentLoggedInUserService> _mockUserService;
        private readonly GetAllWorkspacesQueryHandler _handler;
        private readonly string _testUserId = "test-user-id";
        
        public GetAllWorkspacesQueryHandlerTests()
        {
            _mockWorkspaceRepository = new Mock<IWorkspaceRepository>();
            _mockUserService = new Mock<ICurrentLoggedInUserService>();
            _mockUserService.Setup(s => s.UserId).Returns(_testUserId);

            _handler = new GetAllWorkspacesQueryHandler(
                _mockUserService.Object,
                _mockWorkspaceRepository.Object);
        }
        
        [Fact]
        public async Task Handle_ReturnsWorkspaces_WhenUserHasWorkspaces()
        {
            // Arrange
            var workspaces = new List<Workspace>
            {
                WorkspaceTestMethods.CreateTestWorkspace(_testUserId),
                WorkspaceTestMethods.CreateTestWorkspace(_testUserId)
            };

            _mockWorkspaceRepository
                .Setup(r => r.GetWorkspacesByUserIdAsync(_testUserId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(workspaces);

            // Act
            var result = await _handler.Handle(new GetAllWorkspacesQuery(), CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.Success.ShouldBeTrue();
            result.Workspaces.ShouldNotBeEmpty();
            result.Workspaces.Count.ShouldBe(2);
        }

        [Fact]
        public async Task Handle_ReturnsEmptyList_WhenUserHasNoWorkspaces()
        {
            // Arrange
            var emptyList = new List<Workspace>();
            
            _mockWorkspaceRepository.Setup(r => r.GetWorkspacesByUserIdAsync(_testUserId,
                    It.IsAny<CancellationToken>()))
                    .ReturnsAsync(emptyList);
            
            // Act
            var result = await _handler.Handle(new GetAllWorkspacesQuery(), CancellationToken.None);
            
            // Assert
            result.ShouldNotBeNull();
            result.Success.ShouldBeTrue();
            result.Message.ShouldContain("No workspaces found");
            result.Workspaces.ShouldBeEmpty();
            
        }
        
    }
}
