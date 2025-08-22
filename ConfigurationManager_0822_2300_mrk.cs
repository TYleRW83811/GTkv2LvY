// 代码生成时间: 2025-08-22 23:00:48
using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Essentials;

// ConfigurationManager类用于管理应用程序配置文件
public class ConfigurationManager
{
    // 配置文件的路径
    private readonly string configFilePath;

    // 构造函数，初始化配置文件路径
    public ConfigurationManager(string configFilePath)
    {
        this.configFilePath = configFilePath;
    }

    // 读取配置文件
    public T ReadConfig<T>() where T : class
    {
        try
        {
            if (!File.Exists(configFilePath))
            {
                throw new FileNotFoundException($"配置文件{configFilePath}未找到。");
            }

            // 读取文件内容并反序列化为指定类型T
            var configData = File.ReadAllText(configFilePath);
            return JsonSerializer.Deserialize<T>(configData, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }
        catch (Exception ex)
        {
            // 错误处理
            Console.WriteLine($"读取配置文件时发生错误：{ex.Message}");
            throw;
        }
    }

    // 写入配置文件
    public void WriteConfig<T>(T configData) where T : class
    {
        try
        {
            // 序列化对象为JSON字符串
            var jsonString = JsonSerializer.Serialize(configData, new JsonSerializerOptions { WriteIndented = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            // 写入文件
            File.WriteAllText(configFilePath, jsonString);
        }
        catch (Exception ex)
        {
            // 错误处理
            Console.WriteLine($"写入配置文件时发生错误：{ex.Message}");
            throw;
        }
    }
}
