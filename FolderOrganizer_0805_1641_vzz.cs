// 代码生成时间: 2025-08-05 16:41:22
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FolderOrganizer
{
    /// <summary>
    /// A program to organize file structures within specified folders.
    /// </summary>
    public class FolderOrganizer
    {
        private readonly string _sourceFolder;
        private readonly string _destinationFolder;
        private readonly Dictionary<string, string> _extensionsMap;

        /// <summary>
        /// Initializes a new instance of FolderOrganizer with source and destination folders.
        /// </summary>
        /// <param name="sourceFolder">The source folder to organize files from.</param>
        /// <param name="destinationFolder">The destination folder to organize files into.</param>
        public FolderOrganizer(string sourceFolder, string destinationFolder)
        {
            _sourceFolder = sourceFolder;
            _destinationFolder = destinationFolder;
            _extensionsMap = new Dictionary<string, string>();
        }

        /// <summary>
        /// Sets the mapping of file extensions to their corresponding destination subfolders.
        /// </summary>
        /// <param name="extension">The file extension.</param>
        /// <param name="subFolder">The subfolder name for the given extension.</param>
        public void MapExtension(string extension, string subFolder)
        {
            _extensionsMap[extension] = subFolder;
        }

        /// <summary>
        /// Organizes the files from the source folder to the destination folder based on the extension mapping.
        /// </summary>
        public async Task OrganizeFilesAsync()
        {
            try
            {
                var files = Directory.GetFiles(_sourceFolder);
                foreach (var file in files)
                {
                    var fileInfo = new FileInfo(file);
                    var extension = fileInfo.Extension.ToLowerInvariant();

                    if (_extensionsMap.TryGetValue(extension, out var subFolder))
                    {
                        var destPath = Path.Combine(_destinationFolder, subFolder, fileInfo.Name);
                        var destDir = Path.GetDirectoryName(destPath);

                        if (!Directory.Exists(destDir))
                        {
                            Directory.CreateDirectory(destDir);
                        }

                        File.Move(file, destPath);
                    }
                    else
                    {
                        Console.WriteLine($"No destination folder found for file: {fileInfo.Name}. Skipping...");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during file organization: {ex.Message}");
            }
        }
    }
}
