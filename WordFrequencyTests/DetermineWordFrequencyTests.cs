using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using WordFrequency;
using WordFrequency.Interfaces;
using WordFrequency.Models;

namespace WordFrequencyTests
{
    [TestFixture]
    class DetermineWordFrequencyTests
    {
        [Test]
        public void WordFrequency_ValidWordList_ReturnsValues()
        {
            List<string> test = new List<string>()
            {
                "test"
            };

            IConfiguration configuration = new Configuration();

            DetermineWordFrequency wordFrequency = new DetermineWordFrequency(test, configuration);

            Assert.Multiple(() =>
            {
                Assert.That(wordFrequency.WordList, Is.Not.Null);
                Assert.That(wordFrequency.WordList.Count, Is.EqualTo(1));
                Assert.That(wordFrequency.Frequency, Is.Not.Null);
                Assert.That(wordFrequency.Frequency.Count, Is.EqualTo(0));
                Assert.That(wordFrequency.configuration, Is.Not.Null);
            });
        }

        [Test]
        public void WordFrequency_EmptyWordList_Throws()
        {
            List<string> test = new List<string>();
            IConfiguration configuration = new Configuration();

            ArgumentException argumentException = Assert.Throws<ArgumentException>(() => new DetermineWordFrequency(test, configuration));

            Assert.Multiple(() =>
            {
                Assert.That(argumentException.Message, Is.EqualTo("List of words to calculate word frequency is empty (Parameter 'WordList')"));
                Assert.That(argumentException.ParamName, Is.EqualTo("WordList"));
            });
        }

        [Test]
        public void WordFrequency_SingleWord_Calculates()
        {
            List<string> test = new List<string>();
            for(int i =0; i<10; ++i)
            {
                test.Add("the");
            }

            IConfiguration configuration = new Configuration();

            DetermineWordFrequency determineWordFrequency = new DetermineWordFrequency(test, configuration);
            determineWordFrequency.CalculateWordFrequency();

            Assert.Multiple(() =>
            {
                Assert.That(determineWordFrequency.Frequency.Count, Is.EqualTo(1));
                Assert.True(determineWordFrequency.Frequency.ContainsKey("the"));
                Assert.That(determineWordFrequency.Frequency["the"], Is.EqualTo(10));
            });
        }
        
        [Test]
        public void WordFrequency_MultipleWords_Calculates()
        {
            List<string> test = ReturnTestList();

            IConfiguration configuration = new Configuration();

            DetermineWordFrequency determineWordFrequency = new DetermineWordFrequency(test, configuration);
            determineWordFrequency.CalculateWordFrequency();

            Assert.Multiple(() =>
            {
                Assert.That(determineWordFrequency.Frequency.Count, Is.EqualTo(2));
                Assert.True(determineWordFrequency.Frequency.ContainsKey("the"));
                Assert.That(determineWordFrequency.Frequency["the"], Is.EqualTo(10));
                Assert.True(determineWordFrequency.Frequency.ContainsKey("how"));
                Assert.That(determineWordFrequency.Frequency["how"], Is.EqualTo(5));
            });
        }

        [Test]
        public void ReturnOrderedFrequency_EmptyFrequency_CalculatesWordFrequency()
        {
            List<string> test = ReturnTestList();

            Configuration configuration = new Configuration()
            {
                TopWordCount = 1
            };

            DetermineWordFrequency determineWordFrequency = new DetermineWordFrequency(test, configuration);
            Assume.That(determineWordFrequency.Frequency.Count, Is.EqualTo(0));
            IEnumerable<KeyValuePair<string, int>> results = determineWordFrequency.ReturnOrderedFrequency();

            Assert.Multiple(() =>
            {
                Assert.NotNull(results);
                Assert.That(results.Count, Is.GreaterThan(0));
            });
        }

        [TestCase(0)]
        [TestCase(-1)]
        [Test]
        public void ReturnOrderedFrequency_InvalidWordCount_Throws(int TopWordCount)
        {
            List<string> test = ReturnTestList();

            Configuration configuration = new Configuration()
            {
                TopWordCount = TopWordCount
            };

            DetermineWordFrequency determineWordFrequency = new DetermineWordFrequency(test, configuration);

            Assert.Multiple(() =>
            {
                ArgumentException argumentException = Assert.Throws<ArgumentException>(() => determineWordFrequency.ReturnOrderedFrequency());
                Assert.That(argumentException.Message, Is.EqualTo("Integer is less than or equal to 0 (Parameter 'TopWordCount')"));
                Assert.That(argumentException.ParamName, Is.EqualTo("TopWordCount"));
            });
        }

        [Test]
        public void ReturnOrderedFrequency_ValidInput_ReturnsOrderedList()
        {
            List<string> test = ReturnTestList();

            Configuration configuration = new Configuration()
            {
                TopWordCount = 10
            };

            DetermineWordFrequency determineWordFrequency = new DetermineWordFrequency(test, configuration);
            IEnumerable<KeyValuePair<string, int>> results = determineWordFrequency.ReturnOrderedFrequency();
            bool IsOrdered = true;
            int previous = 0;
            foreach(KeyValuePair<string, int> pair in results)
            {
                //Skip the comparison against previous for the first item
                if(previous == 0)
                {
                    continue;
                }
                if(pair.Value>previous)
                {
                    IsOrdered = false;
                }
                previous = pair.Value;
            }

            Assert.True(IsOrdered);
        }

        public List<string> ReturnTestList()
        {
            List<string> test = new List<string>();
            for (int i = 0; i < 10; ++i)
            {
                test.Add("the");
                if (i % 2 == 0)
                {
                    test.Add("how");
                }
            }

            return test;
        }
    }
}
