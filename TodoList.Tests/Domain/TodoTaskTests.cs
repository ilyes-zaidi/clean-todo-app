using FluentAssertions;
using TodoList.Domain.Entity;
using Xunit;

namespace TodoList.Tests.Domain;

public class TodoTaskTests
{
    [Fact]
    public void CreateTodoTask_ShouldInitializeCorrectly()
    {
        // Arrange
        var title = "Test Task";
        var description = "Test Description";

        // Act
        var task = new TodoTask(title, description);

        // Assert
        task.Id.Should().NotBe(Guid.Empty);
        task.Title.Should().Be(title);
        task.Description.Should().Be(description);
        task.IsCompleted.Should().BeFalse();
    }

    [Fact]
    public void MarkAsCompleted_ShouldSetIsCompletedToTrue()
    {
        // Arrange
        var task = new TodoTask("Test", "Description");

        // Act
        task.MarkAsCompleted();

        // Assert
        task.IsCompleted.Should().BeTrue();
    }
}
