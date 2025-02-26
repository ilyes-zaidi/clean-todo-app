using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Testcontainers.PostgreSql;
using TodoList.Domain.Entity;
using TodoList.Infrastructure.Data;
using TodoList.Infrastructure.Repository;
using Xunit;

namespace TodoList.Tests.Infrastructure;

public class TaskRepositoryTests : IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer;
    private TaskDbContext _dbContext;
    private TaskRepository _repository;

    public TaskRepositoryTests()
    {
        _dbContainer = new PostgreSqlBuilder()
            .WithImage("postgres:latest")
            .WithDatabase("TodoListTest")
            .WithUsername("test")
            .WithPassword("test")
            .Build();
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();

        var options = new DbContextOptionsBuilder<TaskDbContext>()
            .UseNpgsql(_dbContainer.GetConnectionString())
            .Options;

        _dbContext = new TaskDbContext(options);
        await _dbContext.Database.EnsureCreatedAsync();
        _repository = new TaskRepository(_dbContext);
    }

    public async Task DisposeAsync()
    {
        await _dbContext.DisposeAsync();
        await _dbContainer.DisposeAsync();
    }

    [Fact]
    public async Task AddAsync_ShouldAddTaskToDatabase()
    {
        // Arrange
        var task = new TodoTask("Test Task", "Test Description");

        // Act
        await _repository.AddAsync(task);
        var result = await _repository.GetByIdAsync(task.Id);

        // Assert
        result.Should().NotBeNull();
        result!.Title.Should().Be(task.Title);
        result.Description.Should().Be(task.Description);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllTasks()
    {
        // Arrange
        var tasks = new[]
        {
            new TodoTask("Task 1", "Description 1"),
            new TodoTask("Task 2", "Description 2"),
            new TodoTask("Task 3", "Description 3")
        };

        foreach (var task in tasks)
        {
            await _repository.AddAsync(task);
        }

        // Act
        var result = await _repository.GetAllAsync();

        // Assert
        result.Should().HaveCount(3);
        result.Should().BeEquivalentTo(tasks, options => 
            options.Including(x => x.Title)
                   .Including(x => x.Description)
                   .Including(x => x.IsCompleted));
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateExistingTask()
    {
        // Arrange
        var task = new TodoTask("Original Title", "Original Description");
        await _repository.AddAsync(task);

        // Act
        task.MarkAsCompleted();
        await _repository.UpdateAsync(task);
        var result = await _repository.GetByIdAsync(task.Id);

        // Assert
        result.Should().NotBeNull();
        result!.IsCompleted.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveTaskFromDatabase()
    {
        // Arrange
        var task = new TodoTask("Test Task", "Test Description");
        await _repository.AddAsync(task);

        // Act
        await _repository.DeleteAsync(task.Id);
        var result = await _repository.GetByIdAsync(task.Id);

        // Assert
        result.Should().BeNull();
    }
}
