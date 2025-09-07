// 代码生成时间: 2025-09-07 23:50:58
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.IO;
using System.Threading.Tasks;

// 定义一个用于数据库迁移的工具类
public class DatabaseMigrationTool
{
    private readonly IServiceProvider _serviceProvider;

    // 构造函数注入服务提供者
    public DatabaseMigrationTool(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    // 执行迁移的方法
    public async Task MigrateDatabaseAsync<TContext>(string connectionString) where TContext : DbContext
    {
        // 检查连接字符串是否为空
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new ArgumentException("Connection string cannot be empty.", nameof(connectionString));

        try
        {
            // 获取数据库上下文实例
            using var context = _serviceProvider.GetRequiredService<TContext>();

            // 检查数据库是否已经在迁移状态
            if (!context.Database.GetPendingMigrations().Any())
            {
                Console.WriteLine("No pending migrations.");
                return; // 如果没有待处理的迁移，则直接返回
            }

            // 执行迁移
            await context.Database.MigrateAsync();
            Console.WriteLine("Database migration completed successfully.");
        }
        catch (Exception ex)
        {
            // 异常处理，记录错误信息
            Console.WriteLine($"An error occurred during database migration: {ex.Message}");
            throw; // 抛出异常，允许调用者进一步处理
        }
    }
}

// 定义一个类来配置迁移工具服务
public static class MigrationServiceCollectionExtensions
{
    public static IServiceCollection AddDatabaseMigrationTool<TContext>(this IServiceCollection services) where TContext : DbContext
    {
        // 注册数据库上下文
        services.AddDbContext<TContext>(options =>
            options.UseSqlServer(
                "Server=(localdb)\mssqllocaldb;Database=YourDatabase;Trusted_Connection=True;",
                b => b.MigrationsAssembly("YourMigrationAssembly")));

        // 注册迁移工具
        services.AddTransient<DatabaseMigrationTool>();

        return services;
    }
}