using System;

namespace FileSystem.Fluent.Abstractions.Core
{
    public interface IFileInfo
    {
        /// <summary>
        /// The name of the file, including extension if it exists
        /// </summary>
        string Name { get; }
        
        /// <summary>
        /// The full path of the file
        /// </summary>
        string Path { get; }
        
        /// <summary>
        /// The extension of the file
        /// </summary>
        string Extension { get; }
        
        /// <summary>
        /// The date the file was created
        /// </summary>
        DateTime CreatedDate { get; }
        
        /// <summary>
        /// The date the file was last modified
        /// </summary>
        DateTime ModifiedDate { get; }
        
        /// <summary>
        /// The number of bytes in the file
        /// </summary>
        long Length { get; }
    }
}