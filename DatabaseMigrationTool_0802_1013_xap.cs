// 代码生成时间: 2025-08-02 10:13:26
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MAUIApp
{
    public class DatabaseMigrationTool
    {
        private readonly IServiceProvider _serviceProvider;

        public DatabaseMigrationTool(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// 执行数据库迁移
        /// </summary>
        public void MigrateDatabase()
        {
            try
            {
                // 获取数据库上下文
                var dbContext = _serviceProvider.GetRequiredService<MyDbContext>();

                // 检查数据库上下文是否已经与数据库连接
                if (dbContext == null)
                {
                    throw new InvalidOperationException("数据库上下文未初始化。");
                }

                // 应用迁移
                dbContext.Database.Migrate();
                Console.WriteLine("数据库迁移成功。");
            }
            catch (Exception ex)
            {
                // 错误处理
                Console.WriteLine($"数据库迁移失败：{ex.Message}");
            }
        }
    }

    /// <summary>
    /// 数据库上下文
    /// </summary>
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        // 定义你的数据库模型和集合
    }

    // 在MAUI应用程序的启动类中注册和使用DatabaseMigrationTool
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // 添加数据库上下文和迁移工具到服务容器
            services.AddDbContext<MyDbContext>(options =>
                options.UseSqlServer("your_connection_string"));

            services.AddSingleton<DatabaseMigrationTool>();
        }
    }
}
