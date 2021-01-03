using System;

namespace FileSystem.Fluent.Abstractions.Core
{
    public interface IDirectoryInfo
    {
        /// <summary>
        /// The name of the directory
        /// </summary>
        string Name { get; }
        
        /// <summary>
        /// The full path of the directory
        /// </summary>
        string Path { get; }
        
        /// <summary>
        /// The date the directory was created
        /// </summary>
        DateTime CreatedDate { get; }
        
        /// <summary>
        /// The last modification date of the directory
        /// 
        /// </summary>
        DateTime ModifiedDate { get; }
    }
}