using NUnit.Framework;
using System;
using System.IO;
using System.Reflection;
using WordFrequency;
using WordFrequency.Models;

namespace WordFrequencyTests
{
    [TestFixture]
    class TextPreperationTests
    {
        [Test]
        public void TextPreparation_EmptyStopWordPath_Throws()
        {
            Configuration configuration = new Configuration(string.Empty, "test");

            ArgumentException argumentException = Assert.Throws<ArgumentException>(() => new TextPreparation(configuration));
            Assert.That(argumentException.Message, Is.EqualTo("File Path is empty (Parameter 'StopWordPath')"));
            Assert.That(argumentException.ParamName, Is.EqualTo("StopWordPath"));
        }

        [Test]
        public void TextPreperation_NonexistentStopWordPath_Throws()
        {
            Configuration configuration = new Configuration("test.txt", "test");

            ArgumentException argumentException = Assert.Throws<ArgumentException>(() => new TextPreparation(configuration));
            Assert.That(argumentException.Message, Is.EqualTo("File does not exist (Parameter 'StopWordPath')"));
            Assert.That(argumentException.ParamName, Is.EqualTo("StopWordPath"));
        }

        [Test]
        public void TextPerperation_EmptyTextPath_Throws()
        {
            Configuration configuration = new Configuration(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "TestData", "voc.txt"), String.Empty);

            ArgumentException argumentException = Assert.Throws<ArgumentException>(() => new TextPreparation(configuration));
            Assert.That(argumentException.Message, Is.EqualTo("File Path is empty (Parameter 'Text Path')"));
            Assert.That(argumentException.ParamName, Is.EqualTo("Text Path"));
        }

        [Test]
        public void TextPerperation_NonexistentTextPath_Throws()
        {
            Configuration configuration = new Configuration(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "TestData", "voc.txt"), "test");

            ArgumentException argumentException = Assert.Throws<ArgumentException>(() => new TextPreparation(configuration));
            Assert.That(argumentException.Message, Is.EqualTo("File does not exist (Parameter 'Text Path')"));
            Assert.That(argumentException.ParamName, Is.EqualTo("Text Path"));
        }
    }
}
