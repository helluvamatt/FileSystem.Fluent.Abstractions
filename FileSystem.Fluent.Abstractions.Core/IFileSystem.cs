namespace FileSystem.Fluent.Abstractions.Core
{
	/// <summary>
	/// Represents an abstraction to a filesystem
	/// </summary>
	public interface IFileSystem
	{
		/// <summary>
		/// Obtain a reference to a file in a filesystem with the given path
		/// </summary>
		/// <param name="path">Path to the file</param>
		/// <returns>The IFile reference</returns>
		IFile File(string path);

		/// <summary>
		/// Obtain a reference to a directory in a filesystem with the given path
		/// </summary>
		/// <param name="path">Path to the directory</param>
		/// <returns>The IDirectory reference</returns>
		IDirectory Directory(string path);
		
		IDirectory UserTempDirectory { get; }
		
		IDirectory ApplicationDataDirectory { get; }
	}
}
