using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using WordFrequency;

namespace WordFrequencyTests
{
    public class PortStemmerTests
    {

        [Test]
        public void PortStemmer_StemWords_VerifyAlgorithmCorrectness()
        {
            //Read in words to be stemmed
            List<string> voc = File.ReadAllLines(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"TestData\voc.txt")).ToList();
            //Read in expected output 
            List<string> output = System.IO.File.ReadAllLines(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"TestData\output.txt")).ToList();
            PorterStemmer porterStemmer = new PorterStemmer();
            List<string> stemmedWords = new List<string>();

            foreach(string word in voc)
            {
                stemmedWords.Add(porterStemmer.StemWord(word));
            }

            Assert.That(stemmedWords.Count, Is.EqualTo(output.Count));
            Assert.That(stemmedWords.SequenceEqual(output));
        }
    }
}