using NUnit.Framework;
using WordFrequency.Models;

namespace WordFrequencyTests
{
    [TestFixture]
    class ConfigurationTests
    {

        public void Configuration_SetValues_ReturnsValues()
        {
            string stopWordPath = "stopword";
            string textPath = "textpath";
            string outputPath = "outputPath";
            string outputFile = "outputFile";

            Configuration configuration = new Configuration()
            {
                StopWordPath = stopWordPath,
                TextPath = textPath,
                OutputPath = outputPath,
                OutputFile = outputFile
            };

            Assert.Multiple(() =>
            {
                Assert.That(configuration, Is.Not.Null);
                Assert.That(configuration.StopWordPath, Is.EqualTo(stopWordPath));
                Assert.That(configuration.TextPath, Is.EqualTo(textPath));
                Assert.That(configuration.OutputPath, Is.EqualTo(outputPath));
                Assert.That(configuration.OutputFile, Is.EqualTo(outputFile));
            });
            
        }
    }
}
