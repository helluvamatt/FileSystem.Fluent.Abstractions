using FileSystem.Fluent.Abstractions.Core;
using Moq;
using NUnit.Framework;

namespace FileSystem.Fluent.Abstractions.Extensions.Tests
{
    public class FileRelativePathFromTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase("c:/temp/test.txt", "c:/temp", ExpectedResult = "test.txt")]
        [TestCase("c:/temp/subdir/test.txt", "c:/temp", ExpectedResult = "subdir/test.txt")]
        [TestCase("/users/username/test.txt", "/users/username", ExpectedResult = "test.txt")]
        [TestCase("/users/username/test.txt", "/users/", ExpectedResult = "username/test.txt")]
        public string GivenFileAsDescendentOfDirectory_WhenRelativePathFrom_ThenReturnsExpected(string filepath, string dirpath)
        {
            IFile file = BuildMockFileWithPath(filepath);
            IDirectory dir = BuildMockDirectoryWithPath(dirpath);

            return file.RelativePathFrom(dir);
        }

        [TestCase("c:/temp/test.txt", "d:/", ExpectedResult = "c:/temp/test.txt")]
        [TestCase("/users/username/test.txt", "/", ExpectedResult = "/users/username/test.txt")]
        //todo: get the last case working
        public string GivenFileNotDescendingFromDirectory_WhenRelativePathFrom_ThenReturnFilePath(string filepath,
            string dirpath)
        {
            IFile file = BuildMockFileWithPath(filepath);
            IDirectory dir = BuildMockDirectoryWithPath(dirpath);

            return file.RelativePathFrom(dir);
        }

        private IFile BuildMockFileWithPath(string path)
        {
            Mock<IFileInfo> mockFileInfo = new Mock<IFileInfo>();
            mockFileInfo.Setup(f => f.Path).Returns(path);

            Mock<IFile> mockFile = new Mock<IFile>();
            mockFile.Setup(f => f.GetFileInfo()).Returns(mockFileInfo.Object);

            return mockFile.Object;
        }

        private IDirectory BuildMockDirectoryWithPath(string path)
        {
            Mock<IDirectoryInfo> mockDirectoryInfo = new Mock<IDirectoryInfo>();
            mockDirectoryInfo.Setup(d => d.Path).Returns(path);
            
            Mock<IDirectory> mockDir = new Mock<IDirectory>();
            mockDir.Setup(d => d.GetDirectoryInfo()).Returns(mockDirectoryInfo.Object);

            return mockDir.Object;
        }
    }
}