using NUnit.Framework;
using System;
using System.IO;
using System.Reflection;
using WordFrequency;

namespace WordFrequencyTests
{
    public class FileSystemChecksTests
    {
        [Test]
        public void FileChecks_EmptyString_Throws()
        {
            FileSystemChecks fileSystemChecks = new FileSystemChecks();

            Assert.Multiple(() =>
            {
                ArgumentException argumentException = Assert.Throws<ArgumentException>(() => fileSystemChecks.FileChecks(string.Empty, "File"));
                Assert.That(argumentException.Message, Is.EqualTo("File Path is empty (Parameter 'File')"));
                Assert.That(argumentException.ParamName, Is.EqualTo("File"));
            });
        }

        [Test]
        public void FileChecks_NonexistentFile_Throws()
        {
            FileSystemChecks fileSystemChecks = new FileSystemChecks();

            Assert.Multiple(() =>
            {
                ArgumentException argumentException = Assert.Throws<ArgumentException>(() => fileSystemChecks.FileChecks("asdf.txt", "File"));
                Assert.That(argumentException.Message, Is.EqualTo("File does not exist (Parameter 'File')"));
                Assert.That(argumentException.ParamName, Is.EqualTo("File"));
            });
        }

        [Test]
        public void FileChecks_EmptyFile_Throws()
        {
            FileSystemChecks fileSystemChecks = new FileSystemChecks();

            Assert.Multiple(() =>
            {
                ArgumentException argumentException = Assert.Throws<ArgumentException>(() => fileSystemChecks.FileChecks(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "TestData", "Empty.txt"), "File"));
                Assert.That(argumentException.Message, Is.EqualTo("File is empty (Parameter 'File')"));
                Assert.That(argumentException.ParamName, Is.EqualTo("File"));
            });
        }

        [Test]
        public void FileChecks_ValidFile_ThrowsNothing()
        {
            FileSystemChecks fileSystemChecks = new FileSystemChecks();
            Assert.DoesNotThrow(() => fileSystemChecks.FileChecks(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "TestData", "voc.txt"), "File"));
        }

        [Test]
        public void FolderChecks_EmptyString_Throws()
        {
            FileSystemChecks fileSystemChecks = new FileSystemChecks();

            Assert.Multiple(() =>
            {
                ArgumentException argumentException = Assert.Throws<ArgumentException>(() => fileSystemChecks.FolderCheck(string.Empty, "Folder Path"));

                Assert.That(argumentException.Message, Is.EqualTo("Folder Path is empty (Parameter 'Folder Path')"));
                Assert.That(argumentException.ParamName, Is.EqualTo("Folder Path"));
            });
        }

        [Test]
        public void FolderChecks_NonexistentFolder_Throws()
        {
            FileSystemChecks fileSystemChecks = new FileSystemChecks();

            Assert.Multiple(() =>
            {
                ArgumentException argumentException = Assert.Throws<ArgumentException>(() => fileSystemChecks.FolderCheck("asdf", "Folder Path"));

                Assert.That(argumentException.Message, Is.EqualTo("Folder Path does not exist (Parameter 'Folder Path')"));
                Assert.That(argumentException.ParamName, Is.EqualTo("Folder Path"));
            });
        }

        [Test]
        public void FolderChecks_ValidFolder_ThrowsNothing()
        {
            FileSystemChecks fileSystemChecks = new FileSystemChecks();

            Assert.DoesNotThrow(() => fileSystemChecks.FolderCheck("TestData", "Folder"));
        }
    }
}
