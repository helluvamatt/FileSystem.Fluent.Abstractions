using System;
using System.IO;
using FileSystem.Fluent.Abstractions.Core;

namespace FileSystem.Fluent.Abstractions.Net5
{
    public class Net5DirectoryInfo : IDirectoryInfo
    {
        public Net5DirectoryInfo(string path)
        {
            Path = path ?? throw new ArgumentNullException(nameof(path));
        }
        public string Path { get; }
        public DateTime CreatedDate => Directory.GetCreationTime(Path);
        public DateTime ModifiedDate => Directory.GetLastWriteTime(Path);
    }
}