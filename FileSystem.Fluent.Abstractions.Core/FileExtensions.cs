using System;
using System.IO;

namespace FileSystem.Fluent.Abstractions.Core
{
    public static class FileExtensions
    {
        public static IFile ThrowWhenExists(this IFile file, string message = null)
        {
            message = message ?? $"The file '{file}' is not expected to exist";
            
            file.WhenNotExists(f => throw new FileSystemException(message));
            
            return file;
        }

        public static IFile ThrowWhenNotExists(this IFile file, string message = null)
        {
            message = message ?? $"The file '{file}' does not exist";
            
            file.WhenNotExists(f => throw new FileSystemException(message));
            
            return file;
        }

        public static string RelativePathFrom(this IFile file, IDirectory directory)
        {
            string filePath = file.GetFileInfo().Path;
            string dirPath = directory.GetDirectoryInfo().Path;

            int index = filePath.ToLower().IndexOf(dirPath, StringComparison.OrdinalIgnoreCase);

            string relativePath = filePath;
            if (index == 0) //file path starts with directory path
            {
                relativePath = filePath.Substring(dirPath.Length);
                relativePath = relativePath.TrimStart('/', '\\');
            }

            return relativePath;
        }
    }
}