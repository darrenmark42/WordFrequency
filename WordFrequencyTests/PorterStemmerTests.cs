using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using WordFrequency;

namespace WordFrequencyTests
{
    [TestFixture]
    class PorterStemmerTests
    {

        List<string> voc = new List<string>();
        List<string> output = new List<string>();


        [SetUp]
        //The sample data was provided by https://tartarus.org/martin/PorterStemmer/
        public void Init()
        {
            //Words to be stemmed
            voc = File.ReadAllLines(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "TestData", "voc.txt")).ToList();
            //Expected output
            output = System.IO.File.ReadAllLines(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "TestData", "output.txt")).ToList();
        }

        [Test]
        public void PortStemmer_StemWords_VerifyAlgorithmCorrectness()
        {
            PorterStemmer porterStemmer = new PorterStemmer();
            List<string> stemmedWords = new List<string>();

            foreach(string word in voc)
            {
                stemmedWords.Add(porterStemmer.StemWord(word));
            }

            Assert.Multiple(() =>
            {
                Assert.That(stemmedWords.Count, Is.EqualTo(output.Count));
                Assert.That(stemmedWords.SequenceEqual(output));
            });
            
        }
    }
}