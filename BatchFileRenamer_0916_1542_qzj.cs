// 代码生成时间: 2025-09-16 15:42:08
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MAUIApp
{
    public class BatchFileRenamer
    {
        // 构造函数，接收目录路径
        public BatchFileRenamer(string directoryPath)
        {
            DirectoryPath = directoryPath;
        }

        // 目录路径
        private readonly string DirectoryPath;

        // 执行批量重命名操作
        public async Task RenameFilesAsync(string searchPattern, string replacementPattern)
        {
            try
            {
                // 获取目录下所有符合搜索模式的文件
                var files = Directory.GetFiles(DirectoryPath, searchPattern).ToList();

                // 并行处理文件重命名
                await Task.WhenAll(files.Select(file => RenameFileAsync(file, replacementPattern)));
            }
            catch (Exception ex)
            {
                // 错误处理
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        // 单个文件重命名操作
        private async Task RenameFileAsync(string filePath, string replacementPattern)
        {
            // 读取文件名和扩展名
            var fileInfo = new FileInfo(filePath);
            var fileName = fileInfo.Name;
            var fileExtension = fileInfo.Extension;

            // 生成新文件名
            var newFileName = fileName.Replace(replacementPattern, Guid.NewGuid().ToString("N"));

            // 构建新的文件路径
            var newFilePath = Path.Combine(fileInfo.DirectoryName, newFileName);

            // 重命名文件
            await Task.Run(() =>
            {
                File.Move(filePath, newFilePath);
                // 日志记录
                Console.WriteLine($"Renamed {filePath} to {newFilePath}");
            });
        }
    }
}
