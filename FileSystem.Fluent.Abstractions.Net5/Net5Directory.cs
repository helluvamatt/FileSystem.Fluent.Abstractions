using System;
using System.IO;
using FileSystem.Fluent.Abstractions.Core;

namespace FileSystem.Fluent.Abstractions.Net5
{
    public class Net5Directory : IDirectory
    {
        public Net5Directory(string path)
        {
            if (path is null) throw new ArgumentNullException(nameof(path));
            DirectoryInfo = new Net5DirectoryInfo(path);
        }

        public IDirectoryInfo DirectoryInfo { get; }

        public IDirectory TryCreate()
        {
            Directory.CreateDirectory(DirectoryInfo.Path);
            return this;
        }

        public IDirectory Delete()
        {
            Directory.Delete(DirectoryInfo.Path, false);
            return this;
        }

        public IDirectory DeleteRecursive()
        {
            Directory.Delete(DirectoryInfo.Path, true);
            return this;
        }

        public IDirectory ForEachSubDirectory(Action<IDirectory> action)
        {
            string[] directoryNames = Directory.GetDirectories(DirectoryInfo.Path);
            foreach (string directoryName in directoryNames)
            {
                action?.Invoke(new Net5Directory(directoryName));
            }
            return this;
        }

        public IDirectory ForEachFile(Action<IFile> action)
        {
            string[] fileNames = Directory.GetFiles(DirectoryInfo.Path);
            foreach (string fileName in fileNames)
            {
                action?.Invoke(new Net5File(fileName));
            }
            return this;
        }

        public IDirectory WhenExists(Action<IDirectory> action)
        {
            if(Directory.Exists(DirectoryInfo.Path)) action?.Invoke(this);
            return this;
        }

        public IDirectory WhenNotExists(Action<IDirectory> action)
        {
            if(!Directory.Exists(DirectoryInfo.Path)) action?.Invoke(this);
            return this;
        }

        public IDirectory ForParent(Action<IDirectory> action)
        {
            DirectoryInfo parent = Directory.GetParent(DirectoryInfo.Path);
            if (parent is not null)
            {
                action.Invoke(this);
            }
            return this;
        }
    }
}