using System;
using Moq;
using NUnit.Framework;

namespace FileSystem.Fluent.Abstractions.Core.Tests
{
    [TestFixture]
    public class ReadOnlyDirectoryDecoratorTests
    {
        [Test]
        public void WhenCreate_ThenThrowsNotSupportedException()
        {
            IDirectory roDirectory = GetReadOnlyDirectoryDecorator();
            Assert.Throws<NotSupportedException>(() => roDirectory.Create());
        }

        [Test]
        public void WhenDelete_ThenThrowsNotSupportedException()
        {
            IDirectory roDirectory = GetReadOnlyDirectoryDecorator();
            Assert.Throws<NotSupportedException>(() => roDirectory.Delete());
        }

        [Test]
        public void WhenDeleteRecursive_ThenThrowsNotSupportedException()
        {
            IDirectory roDirectory = GetReadOnlyDirectoryDecorator();
            Assert.Throws<NotSupportedException>(() => roDirectory.DeleteRecursive());
        }
        
        private IDirectory GetReadOnlyDirectoryDecorator()
        {
            Mock<IFileSystem> mockFileSystem = new Mock<IFileSystem>();
            Mock<IDirectory> mockDirectory = new Mock<IDirectory>();

            mockFileSystem
                .Setup(fs => fs.Directory(It.IsAny<string>()))
                .Returns(mockDirectory.Object);
            
            IFileSystem roFileSystem = new ReadOnlyFileSystemDecorator(mockFileSystem.Object);
            

            return roFileSystem.Directory("/");
        }

    }
}