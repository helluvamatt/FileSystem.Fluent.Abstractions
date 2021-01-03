using System;
using System.IO;
using FileSystem.Fluent.Abstractions.Core;

namespace FileSystem.Fluent.Abstractions.Physical
{
    public class LocalFileSystem : IFileSystem
    {
        public LocalFileSystem()
        {
            
        }

        public IFileSystem AsReadOnly()
        {
            return new ReadOnlyFileSystemDecorator(this);
        }
        
        public IFile File(string path)
        {
            return new LocalFile(path);
        }

        public IDirectory Directory(string path)
        {
            return new LocalDirectory(path);
        }

        private IDirectory GetSpecialDirectory(Environment.SpecialFolder folder)
        {
            return new LocalDirectory(Environment.GetFolderPath(folder));
        }

        public IDirectory UserTempDirectory => new LocalDirectory(Path.GetTempPath());

        public IDirectory ApplicationDataDirectory => GetSpecialDirectory(Environment.SpecialFolder.ApplicationData);
    }
}