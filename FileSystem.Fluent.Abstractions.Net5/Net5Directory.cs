using System;
using System.IO;
using FileSystem.Fluent.Abstractions.Core;

namespace FileSystem.Fluent.Abstractions.Net5
{
    public class Net5Directory : IDirectory
    {
        public Net5Directory(string path)
        {
            Path = path;
        }

        public string Path { get; }

        public IDirectory TryCreate()
        {
            try
            {
                Directory.CreateDirectory(Path);
                return this;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public IDirectory Delete()
        {
            Directory.Delete(Path, false);
            return this;
        }

        public IDirectory DeleteRecursive()
        {
            Directory.Delete(Path, true);
            return this;
        }

        public IDirectory ForEachSubDirectory(Action<IDirectory> action)
        {
            string[] directoryNames = Directory.GetDirectories(Path);
            foreach (string directoryName in directoryNames)
            {
                action?.Invoke(new Net5Directory(directoryName));
            }
            return this;
        }

        public IDirectory ForEachFile(Action<IFile> action)
        {
            string[] fileNames = Directory.GetFiles(Path);
            foreach (string fileName in fileNames)
            {
                action?.Invoke(new Net5File(fileName));
            }
            return this;
        }

        public IDirectory WhenExists(Action<IDirectory> action)
        {
            if(Directory.Exists(Path)) action?.Invoke(this);
            return this;
        }

        public IDirectory WhenNotExists(Action<IDirectory> action)
        {
            if(!Directory.Exists(Path)) action?.Invoke(this);
            return this;
        }

        public IDirectory ForParent(Action<IDirectory> action)
        {
            DirectoryInfo parent = Directory.GetParent(Path);
            if (parent is not null)
            {
                action.Invoke(this);
            }
            return this;
        }
    }
}