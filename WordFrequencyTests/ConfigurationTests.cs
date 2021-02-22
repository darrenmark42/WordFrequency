using NUnit.Framework;
using WordFrequency.Models;

namespace WordFrequencyTests
{
    [TestFixture]
    class ConfigurationTests
    {
        [TestCase("stopword", "textPath")]
        [TestCase("", "")]
        [TestCase("stopword", "")]
        [TestCase("", "textPath")]
        public void Configuration_SetValues_ReturnsValues(string stopWordPath, string textPath)
        {
            Configuration configuration = new Configuration(stopWordPath, textPath);

            Assert.That(configuration, Is.Not.Null);
            Assert.That(configuration.StopWordPath, Is.EqualTo(stopWordPath));
            Assert.That(configuration.TextPath, Is.EqualTo(textPath));
        }
    }
}
