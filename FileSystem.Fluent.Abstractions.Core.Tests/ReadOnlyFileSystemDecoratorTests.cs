using System;
using System.IO;
using FileSystem.Fluent.Abstractions.Core;
using Moq;
using NUnit.Framework;

namespace FileSystem.Fluent.Abstractions.Extensions.Tests
{
    [TestFixture]
    public class ReadOnlyFileSystemDecoratorTests
    {
        private Mock<IFileSystem> mockFileSystem;

        [SetUp]
        public void TestInitialize()
        {
            mockFileSystem = new Mock<IFileSystem>();
        }
        
        [Test]
        public void WhenConstructedWithNullFileSystem_ThenThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => new ReadOnlyFileSystemDecorator(null));
        }
        
        [Test]
        public void GivenReadOnlyFileSystem_WhenRequestFile_ThenCannotWriteToFile()
        {
            IFileSystem roFileSystem = new ReadOnlyFileSystemDecorator(mockFileSystem.Object);
            
            Mock<IFile> mockFile = new Mock<IFile>();
            mockFileSystem.Setup(fs => fs.File("test.txt")).Returns(mockFile.Object);
            
            IFile file = roFileSystem.File("test.txt");

            Assert.Throws<NotSupportedException>(() => file.WriteContents("anything"));
        }
        
        [Test]
        public void GivenReadOnlyFileSystem_WhenRequestDirectory_ThenCannotDeleteDirectory()
        {
            IFileSystem roFileSystem = new ReadOnlyFileSystemDecorator(mockFileSystem.Object);
            
            Mock<IDirectory> mockDirectory = new Mock<IDirectory>();
            mockFileSystem.Setup(fs => fs.Directory("/")).Returns(mockDirectory.Object);
            
            IDirectory directory = roFileSystem.Directory("/");

            Assert.Throws<NotSupportedException>(() => directory.Delete());
        }
    }
}