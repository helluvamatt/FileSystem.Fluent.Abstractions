using System;

namespace FileSystem.Fluent.Abstractions.Core
{
    /// <summary>
    /// Represents an abstraction to a directory within a filesystem.
    /// </summary>
	public interface IDirectory
	{
        string Path { get; }
        
        /// <summary>
        /// Create the directory as an empty directory if it doesn't exist. Also creates all the parent directories if they do not exist.
        /// </summary>
        /// <returns>The current directory, for chaining.</returns>
        IDirectory Create();

        /// <summary>
        /// Creates an IFile for the given path relative to this IDirectory and returns it.
        /// </summary>
        /// <param name="relativePath">A path to a file relative to the current IDirectory</param>
        /// <returns>A reference to the <c>IFile</c> specified.</returns>
        IFile File(string relativePath);

        /// <summary>
        /// Creates an IDirectory for the given path relative to this IDirectory and returns it.
        /// </summary>
        /// <param name="relativePath">A path to a directory relative to the current IDirectory</param>
        /// <returns>A reference to the <c>IDirectory</c> specified.</returns>
        IDirectory Directory(string relativePath);

        /// <summary>
        /// Delete the directory only. Implementations may throw exceptions or do nothing if the directory is not empty.
        /// </summary>
        /// <returns>The current directory, for chaining.</returns>
        IDirectory Delete();

        /// <summary>
        /// Delete the directory and all child items recursively. This is potentially very destructive, use with caution.
        /// </summary>
        /// <returns>The current directory, for chaining.</returns>
        IDirectory DeleteRecursive();

        /// <summary>
        /// Iterate over a directory's immediate directory children
        /// </summary>
        /// <param name="action">Action to be called for each child subdirectory.</param>
        /// <returns>The current directory, for chaining.</returns>
        IDirectory ForEachSubDirectory(Action<IDirectory> action);

        /// <summary>
        /// Iterate over a directory's immediate file children
        /// </summary>
        /// <param name="action">Action to be called for each child file.</param>
        /// <returns>The current directory, for chaining.</returns>
        IDirectory ForEachFile(Action<IFile> action);

        /// <summary>
        /// If the current directory exists, call the given action.
        /// </summary>
        /// <param name="action">Callback</param>
        /// <returns>The current directory, for chaining.</returns>
        IDirectory WhenExists(Action<IDirectory> action);

        /// <summary>
        /// If the current directory does not exist, call the given action.
        /// </summary>
        /// <param name="action">Callback</param>
        /// <returns>The current directory, for chaining.</returns>
        IDirectory WhenNotExists(Action<IDirectory> action);

        /// <summary>
        /// Get the parent directory of this directory and pass it to the given callback.
        /// </summary>
        /// <param name="action">Callback that receives the parent directory</param>
        /// <returns>The current directory, for chaining.</returns>
        IDirectory ForParent(Action<IDirectory> action);

        IDirectoryInfo GetDirectoryInfo();
    }
}
