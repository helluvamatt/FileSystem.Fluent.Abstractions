using System;

namespace FileSystem.Fluent.Abstractions.Core
{
    public class FileSystemException : Exception
    {
        public FileSystemException() : base() {}
        
        public FileSystemException(string message): base() {}
    }
}