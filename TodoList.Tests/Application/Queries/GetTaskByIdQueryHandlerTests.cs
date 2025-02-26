using AutoMapper;
using FluentAssertions;
using Moq;
using TodoList.Application.Tasks.Queries.GetTasksById;
using TodoList.Domain.Entity;
using TodoList.Domain.Repository;
using Xunit;

namespace TodoList.Tests.Application.Queries;

public class GetTaskByIdQueryHandlerTests
{
    private readonly Mock<ITaskRepository> _mockRepository;
    private readonly GetTaskByIdQueryHandler _handler;

    public GetTaskByIdQueryHandlerTests()
    {
        _mockRepository = new Mock<ITaskRepository>();
        _handler = new GetTaskByIdQueryHandler(_mockRepository.Object, new Mapper(new MapperConfiguration(cfg => { })));
    }

    [Fact]
    public async Task Handle_WithExistingTask_ShouldReturnTaskDto()
    {
        // Arrange
        var taskId = Guid.NewGuid();
        var task = new TodoTask("Test Task", "Test Description") { Id = taskId };
        
        _mockRepository
            .Setup(r => r.GetByIdAsync(taskId))
            .ReturnsAsync(task);

        var query = new GetTaskByIdQuery(taskId);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(taskId);
        result.Title.Should().Be(task.Title);
        result.Description.Should().Be(task.Description);
        result.IsCompleted.Should().Be(task.IsCompleted);
    }

    [Fact]
    public async Task Handle_WithNonExistingTask_ShouldReturnNull()
    {
        // Arrange
        var taskId = Guid.NewGuid();
        _mockRepository
            .Setup(r => r.GetByIdAsync(taskId))
            .ReturnsAsync((TodoTask?)null);

        var query = new GetTaskByIdQuery(taskId);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeNull();
    }
}
