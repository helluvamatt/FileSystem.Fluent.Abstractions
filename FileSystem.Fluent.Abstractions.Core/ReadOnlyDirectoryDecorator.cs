using System;

namespace FileSystem.Fluent.Abstractions.Core
{
    public class ReadOnlyDirectoryDecorator : IDirectory
    {
        private readonly IDirectory directory;

        public ReadOnlyDirectoryDecorator(IDirectory directory)
        {
            this.directory = directory ?? throw new ArgumentNullException(nameof(directory));
        }

        public string Path => this.directory.Path;
        
        public IDirectory Create()
        {
            throw new NotSupportedException("You are using a read-only version of the file system, therefore this operation is disallowed.");
        }

        public IFile File(string relativePath)
        {
            return new ReadOnlyFileDecorator(this.directory.File(relativePath));
        }

        public IDirectory Directory(string relativePath)
        {
            return new ReadOnlyDirectoryDecorator(this.directory.Directory(relativePath));
        }

        public IDirectory Delete()
        {
            throw new NotSupportedException("You are using a read-only version of the file system, therefore this operation is disallowed.");
        }

        public IDirectory DeleteRecursive()
        {
            throw new NotSupportedException("You are using a read-only version of the file system, therefore this operation is disallowed.");
        }

        public IDirectory ForEachSubDirectory(Action<IDirectory> action)
        {
            return this.directory.ForEachSubDirectory(action);
        }

        public IDirectory ForEachFile(Action<IFile> action)
        {
            return this.directory.ForEachFile(action);
        }

        public IDirectory WhenExists(Action<IDirectory> action)
        {
            return this.directory.WhenExists(action);
        }

        public IDirectory WhenNotExists(Action<IDirectory> action)
        {
            return this.directory.WhenNotExists(action);
        }

        public IDirectory ForParent(Action<IDirectory> action)
        {
            return this.directory.ForParent(action);
        }

        public IDirectoryInfo GetDirectoryInfo()
        {
            throw new NotSupportedException();
        }
    }
}