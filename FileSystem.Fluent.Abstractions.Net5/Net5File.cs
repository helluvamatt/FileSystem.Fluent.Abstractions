using System;
using System.IO;
using System.Text;
using FileSystem.Fluent.Abstractions.Core;

namespace FileSystem.Fluent.Abstractions.Net5
{
    public class Net5File : IFile
    {
        public Net5File(string path)
        {
            if(path is null) throw new ArgumentNullException(nameof(path));
            FileInto = new Net5FileInfo(path);
        }

        public IFileInfo FileInto { get; }

        public IFile TryCreate()
        {
            File.Create(FileInto.Path);
            return this;
        }

        public IFile CopyTo(IFile destination, Action<IFile, IFile> postCopyAction = null)
        {
            File.Copy(FileInto.Path, destination.FileInto.Path);
            postCopyAction?.Invoke(this, destination);
            return this;
        }

        public IFile Delete()
        {
            File.Delete(FileInto.Path);
            return this;
        }

        public IFile OpenWrite(Action<Stream> action)
        {
            using Stream fileStream = new FileStream(FileInto.Path, FileMode.OpenOrCreate, FileAccess.Write);
            action?.Invoke(fileStream);
            return this;
        }

        public IFile OpenRead(Action<Stream> action)
        {
            using Stream fileStream = new FileStream(FileInto.Path, FileMode.OpenOrCreate, FileAccess.Read);
            action?.Invoke(fileStream);
            return this;
        }

        public IFile WriteContents(string contents, Encoding encoding = null)
        {
            encoding ??= Encoding.Default;
            byte[] bytes = encoding.GetBytes(contents);
            using Stream fileStream = new FileStream(FileInto.Path, FileMode.Truncate, FileAccess.Write);
            fileStream.Write(bytes);
            return this;
        }

        public IFile AppendContents(string contents, Encoding encoding = null)
        {
            encoding ??= Encoding.Default;
            byte[] bytes = encoding.GetBytes(contents);
            OpenWrite(s =>
            {
                s.Position = s.Length;
                s.Write(bytes);
            });
            return this;
        }

        public IFile ReadLines(Action<string> action, Encoding encoding = null, string newline = null)
        {
            encoding ??= Encoding.Default;
            OpenRead(s =>
            {
                using StreamReader sr = new(s, encoding);
                string lineContent = sr.ReadLine();
                action?.Invoke(lineContent);
            });
            return this;
        }

        public IFile ReadAllLines(Action<string> action, Encoding encoding = null)
        {
            encoding ??= Encoding.Default;
            OpenRead(s =>
            {
                using StreamReader sr = new(s, encoding);
                string contents = sr.ReadToEnd();
                action?.Invoke(contents);
            });
            return this;
        }

        public IFile WhenExists(Action<IFile> action)
        {
            if(File.Exists(FileInto.Path)) action?.Invoke(this);
            return this;
        }

        public IFile WhenNotExists(Action<IFile> action)
        {
            if(!File.Exists(FileInto.Path)) action?.Invoke(this);
            return this;
        }

        public IFile ForParent(Action<IDirectory> action)
        {
            string directoryName = System.IO.Path.GetDirectoryName(FileInto.Path);
            action?.Invoke(new Net5Directory(directoryName));
            return this;
        }
    }
}