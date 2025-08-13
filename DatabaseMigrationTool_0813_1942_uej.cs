// 代码生成时间: 2025-08-13 19:42:51
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

// 定义一个数据库上下文
public class MigrationDbContext : DbContext
{
    public MigrationDbContext(DbContextOptions<MigrationDbContext> options) : base(options)
    {
    }

    // 定义数据库迁移时需要的实体，这里以一个示例Entity为例
    public DbSet<SampleEntity> SampleEntities { get; set; }
}

// 定义实体
public class SampleEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
}

// 数据库迁移工具类
public class DatabaseMigrationTool
{
    private readonly string _connectionString;

    public DatabaseMigrationTool(string connectionString)
    {
        _connectionString = connectionString;
    }

    // 执行数据库迁移
    public void MigrateDatabase()
    {
        try
        {
            var optionsBuilder = new DbContextOptionsBuilder<MigrationDbContext>();
            optionsBuilder.UseSqlServer(_connectionString);
            var options = optionsBuilder.Options;
            var context = new MigrationDbContext(options);

            context.Database.Migrate();
            Console.WriteLine("Database migration completed successfully.");
        }
        catch (Exception ex)
        {
            // 错误处理
            Console.WriteLine($"An error occurred during database migration: {ex.Message}");
            throw;
        }
    }
}

// 示例用法
public class Program
{
    public static void Main(string[] args)
    {
        string connectionString = "YourConnectionStringHere"; // 替换为实际的数据库连接字符串
        var migrationTool = new DatabaseMigrationTool(connectionString);
        migrationTool.MigrateDatabase();
    }
}