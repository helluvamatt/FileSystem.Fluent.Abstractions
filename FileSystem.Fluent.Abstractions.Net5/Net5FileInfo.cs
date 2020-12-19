using System;
using System.IO;
using FileSystem.Fluent.Abstractions.Core;

namespace FileSystem.Fluent.Abstractions.Net5
{
    public class Net5FileInfo : IFileInfo
    {
        public Net5FileInfo(string path)
        {
            Path = path ?? throw new ArgumentNullException(nameof(path));
        }
        public string Path { get; }
        public DateTime CreatedDate => File.GetCreationTime(Path);
        public DateTime ModifiedDate => File.GetLastWriteTime(Path);
    }
}