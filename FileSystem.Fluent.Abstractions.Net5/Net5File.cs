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
            Path = path ?? throw new ArgumentNullException(nameof(path));
        }

        public string Path { get; }

        public IFile TryCreate()
        {
            try
            {
                FileStream fileStream = File.Create(Path);
                fileStream.Close();
                return this;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public IFile CopyTo(IFile destination, Action<IFile, IFile> postCopyAction = null)
        {
            File.Copy(Path, destination.Path);
            postCopyAction?.Invoke(this, destination);
            return this;
        }

        public IFile Delete()
        {
            File.Delete(Path);
            return this;
        }

        public IFile OpenWrite(Action<Stream> action)
        {
            using Stream fileStream = new FileStream(Path, FileMode.OpenOrCreate, FileAccess.Write);
            action?.Invoke(fileStream);
            return this;
        }

        public IFile OpenRead(Action<Stream> action)
        {
            using Stream fileStream = new FileStream(Path, FileMode.OpenOrCreate, FileAccess.Read);
            action?.Invoke(fileStream);
            return this;
        }

        public IFile WriteContents(string contents, Encoding encoding = null)
        {
            byte[] bytes = Encoding.Default.GetBytes(contents);
            using Stream fileStream = new FileStream(Path, FileMode.Truncate, FileAccess.Write);
            fileStream.Write(bytes);
            return this;
        }

        public IFile AppendContents(string contents, Encoding encoding = null)
        {
            byte[] bytes = Encoding.Default.GetBytes(contents);
            OpenWrite(s =>
            {
                s.Position = s.Length;
                s.Write(bytes);
            });
            return this;
        }

        public IFile ReadLines(Action<string> action, Encoding encoding = null, string newline = null)
        {
            OpenRead(s =>
            {
                using StreamReader sr = new(s);
                string lineContent = sr.ReadLine();
                action?.Invoke(lineContent);
            });
            return this;
        }

        public IFile ReadAllLines(Action<string> action, Encoding encoding = null)
        {
            OpenRead(s =>
            {
                using StreamReader sr = new(s);
                string contents = sr.ReadToEnd();
                action?.Invoke(contents);
            });
            return this;
        }

        public IFile WhenExists(Action<IFile> action)
        {
            if(File.Exists(Path)) action?.Invoke(this);
            return this;
        }

        public IFile WhenNotExists(Action<IFile> action)
        {
            if(!File.Exists(Path)) action?.Invoke(this);
            return this;
        }

        public IFile ForParent(Action<IDirectory> action)
        {
            string directoryName = System.IO.Path.GetDirectoryName(Path);
            action?.Invoke(new Net5Directory(directoryName));
            return this;
        }
    }
}