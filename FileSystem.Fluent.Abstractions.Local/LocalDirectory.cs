using System;
using FileSystem.Fluent.Abstractions.Core;

namespace FileSystem.Fluent.Abstractions.Physical
{
    public class LocalDirectory : IDirectory
    {
        public LocalDirectory(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(path));
            
            this.Path = path;
        }

        public string Path { get; }

        public IDirectory Create()
        {
            throw new NotImplementedException();
        }

        public IFile File(string relativePath)
        {
            return new LocalFile(System.IO.Path.Combine(this.Path, relativePath));
        }

        public IDirectory Directory(string relativePath)
        {
            throw new NotImplementedException();
        }

        public IDirectory Delete()
        {
            throw new NotImplementedException();
        }

        public IDirectory DeleteRecursive()
        {
            throw new NotImplementedException();
        }

        public IDirectory ForEachSubDirectory(Action<IDirectory> action)
        {
            throw new NotImplementedException();
        }

        public IDirectory ForEachFile(Action<IFile> action)
        {
            throw new NotImplementedException();
        }

        public IDirectory WhenExists(Action<IDirectory> action)
        {
            throw new NotImplementedException();
        }

        public IDirectory WhenNotExists(Action<IDirectory> action)
        {
            throw new NotImplementedException();
        }

        public IDirectory ForParent(Action<IDirectory> action)
        {
            throw new NotImplementedException();
        }

        public IDirectoryInfo GetDirectoryInfo()
        {
            throw new NotImplementedException();
        }
    }
}