using FluentAssertions;
using Moq;
using TodoList.Application.Tasks.Commands.AddTask;
using TodoList.Domain.Entity;
using TodoList.Domain.Repository;
using Xunit;

namespace TodoList.Tests.Application.Commands;

public class AddTaskCommandHandlerTests
{
    private readonly Mock<ITaskRepository> _mockRepository;
    private readonly AddTaskCommandHandler _handler;

    public AddTaskCommandHandlerTests()
    {
        _mockRepository = new Mock<ITaskRepository>();
        _handler = new AddTaskCommandHandler(_mockRepository.Object);
    }

    [Fact]
    public async Task Handle_ShouldAddTaskAndReturnId()
    {
        // Arrange
        var command = new AddTaskCommand("Test Task", "Test Description");
        TodoTask? capturedTask = null;

        _mockRepository
            .Setup(r => r.AddAsync(It.IsAny<TodoTask>()))
            .Callback<TodoTask>(task => capturedTask = task);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBe(Guid.Empty);
        _mockRepository.Verify(r => r.AddAsync(It.IsAny<TodoTask>()), Times.Once);
        
        capturedTask.Should().NotBeNull();
        capturedTask!.Title.Should().Be(command.Title);
        capturedTask.Description.Should().Be(command.Description);
        capturedTask.IsCompleted.Should().BeFalse();
    }
}
