using System;
using System.IO;
using System.Text;
using FileSystem.Fluent.Abstractions.Core;

namespace FileSystem.Fluent.Abstractions.Physical
{
    public class LocalFile : IFile
    {
        public LocalFile(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(path));
            
            this.Path = path;
        }

        public string Path { get; }

        public IFile TryCreate()
        {
            throw new NotImplementedException();
        }

        public IFile CopyTo(string destinationPath, Action<IFile, IFile> postCopyAction = null)
        {
            LocalFile destination = new LocalFile(destinationPath);
            return this.CopyTo(destination, postCopyAction);
        }

        public IFile CopyTo(IFile destination, Action<IFile, IFile> postCopyAction = null)
        {
            File.Copy(this.Path, destination.Path);
            postCopyAction?.Invoke(this, destination);
            return this;
        }

        public IFile MoveTo(string destinationPath, Action<IFile, IFile> postMoveAction)
        {
            File.Move(this.Path, destinationPath);
            return new LocalFile(destinationPath);
        }

        public IFile Delete()
        {
            File.Delete(this.Path);
            return this;
        }

        public IFile OpenWrite(Action<Stream> action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            using (Stream stream = File.OpenWrite(this.Path))
            {
                action(stream);
            }

            return this;
        }

        public IFile OpenRead(Action<Stream> action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            using (Stream stream = File.OpenRead(this.Path))
            {
                action(stream);
            }

            return this;
        }

        public IFile WriteContents(string contents, Encoding encoding = null)
        {
            File.WriteAllText(this.Path, contents, encoding ?? Encoding.UTF8);
            return this;
        }

        public IFile AppendContents(string contents, Encoding encoding = null)
        {
            File.AppendAllText(this.Path, contents, encoding ?? Encoding.UTF8);
            return this;
        }

        public IFile ReadLines(Action<string> action, Encoding encoding = null, string newline = null)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
            
            foreach (string line in File.ReadLines(this.Path, encoding ?? Encoding.UTF8))
            {
                action(line);
            }

            return this;
        }

        public IFile ReadAllLines(Action<string> action, Encoding encoding = null)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            action(File.ReadAllText(this.Path, encoding ?? Encoding.UTF8));

            return this;
        }

        public IFile WhenExists(Action<IFile> action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
            
            if (File.Exists(this.Path))
            {
                action(this);
            }

            return this;
        }

        public IFile WhenNotExists(Action<IFile> action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
            
            if (!File.Exists(this.Path))
            {
                action(this);
            }

            return this;
        }

        public IFile ForParent(Action<IDirectory> action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
            
            IDirectory parent = new LocalDirectory(new FileInfo(this.Path).Directory.FullName);
            action(parent);

            return this;
        }

        public IFileInfo GetFileInfo()
        {
            throw new NotImplementedException();
        }
    }
}