using System;

namespace FileSystem.Fluent.Abstractions.Core
{
    public interface IDirectoryInfo
    {
        string Path { get; }
        
        DateTime CreatedDate { get; }
        
        DateTime ModifiedDate { get; }        
    }
}