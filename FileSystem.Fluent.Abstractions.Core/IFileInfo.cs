using System;

namespace FileSystem.Fluent.Abstractions.Core
{
    public interface IFileInfo
    {
        string Path { get; }
        
        DateTime CreatedDate { get; }
        
        DateTime ModifiedDate { get; }
    }
}