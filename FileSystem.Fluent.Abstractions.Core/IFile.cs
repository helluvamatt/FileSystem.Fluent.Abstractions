using System;
using System.IO;
using System.Text;

namespace FileSystem.Fluent.Abstractions.Core
{
    /// <summary>
    /// Represents an abstraction to a single file in a filesystem
    /// </summary>
	public interface IFile
	{
        string Path { get; }
        
        /// <summary>
        /// Try to create the file
        /// </summary>
        /// <remarks>
        /// <para>This will create the file as a zero-length empty file. If the file exists, it will be truncated.</para>
        /// <para>If, for some reason, this operation fails, the file may be left in an unknown state, and subsequent actions may fail.</para>
        /// </remarks>
        /// <returns>The current file, for chaining</returns>
        IFile TryCreate();

        /// <summary>
        /// Copy the contents of the current File to the given destination
        /// </summary>
        /// <param name="destinationPath">The path to copy the file to. The path is interpreted as relative to the source file's parent directory if an absolute path is not given.</param>
        /// <param name="postCopyAction">Action to perform after the copy is complete. This is passed the source file as the first argument, and the destination file as the second argument.</param>
        /// <returns>The current file, for chaining</returns>
        IFile CopyTo(string destinationPath, Action<IFile, IFile> postCopyAction = null);

        /// <summary>
        /// Copy the contents of the current File to the given destination
        /// </summary>
        /// <param name="destination">Destination file</param>
        /// <param name="postCopyAction">Action to perform after the copy is complete. This is passed the source file as the first argument, and the destination file as the second argument.</param>
        /// <returns>The current file, for chaining</returns>
        IFile CopyTo(IFile destination, Action<IFile, IFile> postCopyAction = null);

        /// <summary>
        /// Moves the given file to a new location. This will create all parent directories of the new path if they do not exist.
        /// </summary>
        /// <param name="destinationPath">The path to move the file to. The path is interpreted as relative to the source file's parent directory if an absolute path is not given.</param>
        /// <param name="postMoveAction">Action to perform after the move is complete. This is passed the original <c>IFile</c> as the first argument and the <c>IFile</c> of the destination as the second argument.</param>
        /// <returns>The current file, for chaining</returns>
        /// <exception cref="FileNotFoundException">Throws a <c>FileNotFoundException</c> if the source file does not exist</exception>
        IFile MoveTo(string destinationPath, Action<IFile, IFile> postMoveAction);

        /// <summary>
        /// Deletes the file
        /// </summary>
        /// <remarks>
        /// Note that after this call returns, any internal file handles or streams may be invalid. You should follow this call immediately with a new call to OpenWrite or Create
        /// </remarks>
        /// <returns>The current file, for chaining</returns>
        IFile Delete();

        /// <summary>
        /// Open the file for writing, calling the given action with the stream
        /// </summary>
        /// <remarks>
        /// Note that when the callback returns, the stream is disposed, so do not save the stream outside of the callback.
        /// </remarks>
        /// <param name="action">Action to be executed while the stream is open.</param>
        /// <returns>The current file, for chaining</returns>
        IFile OpenWrite(Action<Stream> action);

        /// <summary>
        /// Open a file for reading, calling the given action with the stream
        /// </summary>
        /// <remarks>
        /// Note that when the callback returns, the stream is disposed, so do not save the stream outside of the callback.
        /// </remarks>
        /// <param name="action">Action to be executed while the stream is open.</param>
        /// <returns>The current file, for chaining</returns>
        IFile OpenRead(Action<Stream> action);

        /// <summary>
        /// Write the given contents to the file using the given encoding, the file is either created or truncated to zero bytes and overwritten.
        /// </summary>
        /// <param name="contents">String contents to write to the file</param>
        /// <param name="encoding">Text encoding to use, defaults to UTF-8</param>
        /// <returns>The current file, for chaining</returns>
        IFile WriteContents(string contents, Encoding encoding = null);

        /// <summary>
        /// Append the given contents to the file using the given encoding. The file will be created if it does not exist.
        /// </summary>
        /// <param name="contents">String contents to append to the file</param>
        /// <param name="encoding">Text encoding to use, defaults to UTF-8</param>
        /// <returns>The current file, for chaining</returns>
        IFile AppendContents(string contents, Encoding encoding = null);

        /// <summary>
        /// Read the file line by line. The given callback is called repeatedly for each line in the file.
        /// </summary>
        /// <param name="action">Line callback</param>
        /// <param name="encoding">File encoding, pass null to attempt to detect the encoding, with fallback to UTF-8</param>
        /// <param name="newline">Line separator string, defaults to <see cref="Environment.NewLine" /></param>
        /// <returns>The current file, for chaining</returns>
        IFile ReadLines(Action<string> action, Encoding encoding = null, string newline = null);

        /// <summary>
        /// Read the entire file into a string, using the given encoding.
        /// </summary>
        /// <param name="action">Contents callback</param>
        /// <param name="encoding">File encoding, pass null to attempt to detect the encoding, with fallback to UTF-8</param>
        /// <returns>The current file, for chaining</returns>
        IFile ReadAllLines(Action<string> action, Encoding encoding = null);

        /// <summary>
        /// If the file exists, call the given callback
        /// </summary>
        /// <param name="action">Callback</param>
        /// <returns>The current file, for chaining</returns>
        IFile WhenExists(Action<IFile> action);

        /// <summary>
        /// If the file does not exist, call the given callback
        /// </summary>
        /// <param name="action">Callback</param>
        /// <returns>The current file, for chaining</returns>
        IFile WhenNotExists(Action<IFile> action);

        /// <summary>
        /// Get the parent directory of this directory and pass it to the given callback.
        /// </summary>
        /// <param name="action">Callback that receives the parent directory</param>
        /// <returns>The current directory, for chaining.</returns>
        IFile ForParent(Action<IDirectory> action);

        IFileInfo GetFileInfo();
    }
}
