using System;

namespace FileSystem.Fluent.Abstractions.Core
{
    public class ReadOnlyFileSystemDecorator : IFileSystem
    {
        private readonly IFileSystem fileSystem;

        public ReadOnlyFileSystemDecorator(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        }

        public IFile File(string path)
        {
            return new ReadOnlyFileDecorator(this.fileSystem.File(path));
        }

        public IDirectory Directory(string path)
        {
            return new ReadOnlyDirectoryDecorator(this.fileSystem.Directory(path));
        }

        public IFileSystem AsReadOnly()
        {
            return this;
        }

        public IDirectory UserTempDirectory { get; }
        public IDirectory ApplicationDataDirectory { get; }
    }
}