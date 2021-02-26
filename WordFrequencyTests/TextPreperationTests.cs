using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using WordFrequency;
using WordFrequency.Models;

namespace WordFrequencyTests
{
    [TestFixture]
    class TextPreperationTests
    {

        [Test]
        public void TextPreparation_ValidPath_ReturnsValues()
        {
            Configuration configuration = new Configuration()
            {
                TextPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "TestData", "Text1.txt"),
                StopWordPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "TestData", "stopwords.txt")
            };

            TextPreparation textPreparation = new TextPreparation(configuration);

            bool textAllLower = CheckForUpperCase(textPreparation.text);
            bool stopWordsAllLower = CheckForUpperCase(textPreparation.stopWords);

            Assert.Multiple(() =>
            {

            });

            Assert.That(textPreparation.text, Is.Not.Empty);
            Assert.That(textPreparation.text.Count, Is.GreaterThan(0));
            Assert.That(textAllLower, Is.True);
            Assert.That(stopWordsAllLower, Is.True);
            Assert.That(textPreparation.stopWords, Is.Not.Empty);
            Assert.That(textPreparation.stopWords.Count, Is.GreaterThan(0));
        }

        private bool CheckForUpperCase(List<string> words)
        {
            bool allLower = true;
            foreach(string word in words)
            {
                foreach(char c in word)
                {
                    if(Char.IsUpper(c))
                    {
                        allLower = false;
                        return allLower;
                    }
                }
            }

            return allLower;
        }

        [Test]
        public void RemoveStopWords_ValidStopWords_RemovesWords()
        {
            List<string> stopwords = new List<string>()
            {
                "fox"
            };

            List<string> text = new List<string>()
            {
                "the",
                "quick",
                "brown",
                "fox"
            };

            TextPreparation textPreparation = new TextPreparation()
            {
                stopWords = stopwords,
                text = text
            };

            textPreparation.RemoveStopWords();


            Assert.Multiple(() =>
            {
                Assert.False(textPreparation.text.Contains("fox"));
                Assert.That(textPreparation.text.Count, Is.EqualTo(3));
            });
        }

        [Test]
        public void RemoveStopWords_EmptyString_RemovesNothing()
        {
            List<string> stopwords = new List<string>()
            {
                string.Empty
            };

            List<string> text = new List<string>()
            {
                "the",
                "quick",
                "brown",
                "fox"
            };

            TextPreparation textPreparation = new TextPreparation()
            {
                stopWords = stopwords,
                text = text
            };

            textPreparation.RemoveStopWords();

            Assert.That(textPreparation.text.Count, Is.EqualTo(text.Count));
        }

        [Test]
        public void RemoveStopWords_Space_RemovesNothing()
        {
            List<string> stopwords = new List<string>()
            {
                " "
            };

            List<string> text = new List<string>()
            {
                "the",
                "quick",
                "brown",
                "fox"
            };

            TextPreparation textPreparation = new TextPreparation()
            {
                stopWords = stopwords,
                text = text
            };

            textPreparation.RemoveStopWords();

            Assert.That(textPreparation.text.Count, Is.EqualTo(text.Count));
        }

        [Test]
        public void RemoveStopWords_NoMatches_RemovesNothing()
        {
            List<string> stopwords = new List<string>()
            {
                "a"
            };

            List<string> text = new List<string>()
            {
                "the",
                "quick",
                "brown",
                "fox"
            };


            TextPreparation textPreparation = new TextPreparation()
            {
                stopWords = stopwords,
                text = text
            };

            textPreparation.RemoveStopWords();

            Assert.That(textPreparation.text.Count, Is.EqualTo(text.Count));
        }

        [Test]
        public void ReturnAlphanumericCharacters_ValidText_RemovesPuncuation()
        {
            List<string> text = new List<string>()
            {
                "hello.",
                ".",
                "@#$a"
            };

            TextPreparation textPreparation = new TextPreparation()
            {
                text = new List<string>(text)
            };

            
            textPreparation.ReturnAlphanumericCharacters();

            bool allAlphanumeric = IsAllAlphanumeric(textPreparation.text);


            Assert.Multiple(() =>
            {
                Assert.That(textPreparation.text.Count, Is.EqualTo(2));
                Assert.True(allAlphanumeric);
                Assert.False(text.SequenceEqual(textPreparation.text));
            });
        }

        [Test]
        public void ReturnAlphanumericCharacters_AllPuncuation_RemovesEverything()
        {
            List<string> text = new List<string>()
            {
                "$%^&*",
                ".",
                " ",
                "",
                "?/"
            };

            TextPreparation textPreparation = new TextPreparation()
            {
                text = new List<string>(text)
            };

            textPreparation.ReturnAlphanumericCharacters();


            Assert.Multiple(() =>
            {
                Assert.That(textPreparation.text.Count, Is.EqualTo(0));
                Assert.False(text.SequenceEqual(textPreparation.text));

            });
        }

        [Test]
        public void ReturnAlphanumericCharacters_NoPuncuation_RemovesNothing()
        {
            List<string> text = new List<string>()
            {
                "the",
                "quick",
                "brown",
                "fox",
                "8"
            };

            TextPreparation textPreparation = new TextPreparation()
            {
                text = new List<string>(text)
            };

            textPreparation.ReturnAlphanumericCharacters();

            Assert.Multiple(() =>
            {
                Assert.That(textPreparation.text.Count, Is.EqualTo(text.Count));
                Assert.True(textPreparation.text.SequenceEqual(text));
            });
        }

        private bool IsAllAlphanumeric(List<string> text)
        {

            foreach (string word in text)
            {
                foreach (char c in word)
                {
                    if (!Char.IsLetterOrDigit(c))
                    {
                        return false;             
                    }
                }
            }

            return true;
        }
    }
}
