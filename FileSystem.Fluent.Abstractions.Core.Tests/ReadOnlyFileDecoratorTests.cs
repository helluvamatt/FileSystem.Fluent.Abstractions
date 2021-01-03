using System;
using Moq;
using NUnit.Framework;

namespace FileSystem.Fluent.Abstractions.Core.Tests
{
    [TestFixture]
    public class ReadOnlyFileDecoratorTests
    {
        [Test]
        public void WhenDelete_ThenThrowsNotSupportedException()
        {
            IFile roFile = GetReadOnlyFileDecorator();
            Assert.Throws<NotSupportedException>(() => roFile.Delete());
        }
        
        [Test]
        public void WhenWriteContents_ThenThrowsNotSupportedException()
        {
            IFile roFile = GetReadOnlyFileDecorator();
            Assert.Throws<NotSupportedException>(() => roFile.WriteContents("anything"));
        }
   
        [Test]
        public void WhenAppendContents_ThenThrowsNotSupportedException()
        {
            IFile roFile = GetReadOnlyFileDecorator();
            Assert.Throws<NotSupportedException>(() => roFile.AppendContents("anything"));
        }

        [Test]
        public void WhenTryCreate_ThenThrowsNotSupportedException()
        {
            IFile roFile = GetReadOnlyFileDecorator();
            Assert.Throws<NotSupportedException>(() => roFile.TryCreate());
        }

        [Test]
        public void WhenCopyToPath_ThenThrowsNotSupportedException()
        {
            IFile roFile = GetReadOnlyFileDecorator();
            Assert.Throws<NotSupportedException>(() => roFile.CopyTo("anything", (src, dest) => { }));
        }

        [Test]
        public void WhenCopyToFile_ThenThrowsNotSupportedException()
        {
            IFile roFile = GetReadOnlyFileDecorator();
            Assert.Throws<NotSupportedException>(() => roFile.CopyTo(roFile, (src, dest) => { }));
        }

        [Test]
        public void WhenMoveToPath_ThenThrowsNotSupportedException()
        {
            IFile roFile = GetReadOnlyFileDecorator();
            Assert.Throws<NotSupportedException>(() => roFile.MoveTo("anywhere", (src, dest) => { }));
        }

        [Test]
        public void WhenOpenWrite_ThenThrowsNotSupportedException()
        {
            IFile roFile = GetReadOnlyFileDecorator();
            Assert.Throws<NotSupportedException>(() => roFile.OpenWrite(stream => { }));
        }

        private IFile GetReadOnlyFileDecorator()
        {
            Mock<IFileSystem> mockFileSystem = new Mock<IFileSystem>();
            Mock<IFile> mockFile = new Mock<IFile>();

            mockFileSystem
                .Setup(fs => fs.File(It.IsAny<string>()))
                .Returns(mockFile.Object);
            
            IFileSystem roFileSystem = new ReadOnlyFileSystemDecorator(mockFileSystem.Object);
            

            return roFileSystem.File(Guid.NewGuid().ToString());
        }
        
    }
}