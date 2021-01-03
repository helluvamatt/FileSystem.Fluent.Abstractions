using System;
using System.IO;
using System.Security;
using System.Text;

namespace FileSystem.Fluent.Abstractions.Core
{
    internal class ReadOnlyFileDecorator : IFile
    {
        private readonly IFile file;

        public ReadOnlyFileDecorator(IFile file)
        {
            this.file = file ?? throw new ArgumentNullException(nameof(file));
        }

        public string Path => this.file.Path;
        
        public IFile TryCreate()
        {
            throw new NotSupportedException("You are using a read-only version of the file system, therefore this operation is disallowed.");
        }

        public IFile CopyTo(string destinationPath, Action<IFile, IFile> postCopyAction = null)
        {
            throw new NotSupportedException("You are using a read-only version of the file system, therefore this operation is disallowed.");
        }

        public IFile CopyTo(IFile destination, Action<IFile, IFile> postCopyAction = null)
        {
            throw new NotSupportedException("You are using a read-only version of the file system, therefore this operation is disallowed.");
        }

        public IFile MoveTo(string destinationPath, Action<IFile, IFile> postMoveAction)
        {
            throw new NotSupportedException("You are using a read-only version of the file system, therefore this operation is disallowed.");
        }

        public IFile Delete()
        {
            throw new NotSupportedException("You are using a read-only version of the file system, therefore this operation is disallowed.");
        }

        public IFile OpenWrite(Action<Stream> action)
        {
            throw new NotSupportedException("You are using a read-only version of the file system, therefore this operation is disallowed.");
        }

        public IFile OpenRead(Action<Stream> action)
        {
            return this.file.OpenRead(action);
        }

        public IFile WriteContents(string contents, Encoding encoding = null)
        {
            throw new NotSupportedException("You are using a read-only version of the file system, therefore this operation is disallowed.");
        }

        public IFile AppendContents(string contents, Encoding encoding = null)
        {
            throw new NotSupportedException("You are using a read-only version of the file system, therefore this operation is disallowed.");
        }

        public IFile ReadLines(Action<string> action, Encoding encoding = null, string newline = null)
        {
            return this.file.ReadLines(action, encoding, newline);
        }

        public IFile ReadAllLines(Action<string> action, Encoding encoding = null)
        {
            return this.file.ReadAllLines(action, encoding);
        }

        public IFile WhenExists(Action<IFile> action)
        {
            return this.file.WhenExists(action);
        }

        public IFile WhenNotExists(Action<IFile> action)
        {
            return this.file.WhenNotExists(action);
        }

        public IFile ForParent(Action<IDirectory> action)
        {
            return this.file.ForParent(action);
        }

        public IFileInfo GetFileInfo()
        {
            return this.file.GetFileInfo();
        }
    }
}