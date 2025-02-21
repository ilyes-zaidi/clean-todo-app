

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TodoList.Domain.Repository;
using TodoList.Infrastructure.Data;
using TodoList.Infrastructure.Repository;

namespace TodoList.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<TaskDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        
        services.AddTransient<ITaskRepository, TaskRepository>();

        return services;
    }
}