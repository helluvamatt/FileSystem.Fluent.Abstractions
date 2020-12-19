using FileSystem.Fluent.Abstractions.Core;

namespace FileSystem.Fluent.Abstractions.Net5
{
    public class Net5FileSystem : IFileSystem
    {
        public IFile File(string path)
        {
            return new Net5File(path);
        }

        public IDirectory Directory(string path)
        {
            return new Net5Directory(path);
        }
    }
}