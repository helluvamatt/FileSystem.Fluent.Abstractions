using System;

namespace FileSystem.Fluent.Abstractions.Core
{
	public static class DirectoryExtensions
	{
        /// <summary>
        /// Iterate over a directory recursively, calling the given action for each child subdirectory.
        /// </summary>
        /// <remarks>
        /// This is a pre-order depth-first traversal, where parent nodes are visited first, followed by child nodes. The action is called on children of the directory, but not the directory itself.
        /// </remarks>
        /// <param name="action">Action to invoke on each child directory</param>
        /// <returns>The current directory, for chaining.</returns>
        public static IDirectory ForEachRecursiveSubDirectory(this IDirectory directory, Action<IDirectory> action)
		{
            return directory.ForEachSubDirectory(child =>
            {
                action.Invoke(child);
                child.ForEachRecursiveSubDirectory(action);
            });
		}

        /// <summary>
        /// Iterate over a directory recursively, calling the given action for each child file.
        /// </summary>
        /// <remarks>
        /// This is a pre-order depth-first traversal. Files in the current directory are iterated first, then each directory is traversed, recursively calling this method on the child directory.
        /// </remarks>
        /// <param name="action">Action to invoke on each child file</param>
        /// <returns>The current directory, for chaining.</returns>
        public static IDirectory ForEachRecursiveFile(this IDirectory directory, Action<IFile> action)
		{
            return directory
                .ForEachFile(action)
                .ForEachSubDirectory(child => child.ForEachRecursiveFile(action));
		}
    }
}
