using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WordFrequency.Interfaces;

namespace WordFrequency
{
    public class DetermineWordFrequency : IDetermineWordFrequency
    {
        public Dictionary<string, int> Frequency { get; set; }

        public List<string> WordList { get; set; }

        public IConfiguration configuration { get; set; }

        public DetermineWordFrequency(List<string> WordList, IConfiguration configuration)
        { 

            if (WordList.Count == 0)
            {
                throw new ArgumentException(string.Format("List of words to calculate word frequency is empty"), "WordList");
            }

            this.WordList= WordList;
              
            Frequency = new Dictionary<string, int>();
            this.configuration = configuration;
        }

        public void CalculateWordFrequency()
        {
           foreach(string word in WordList)
            {
                //If the word has already been encountered, increment the count
                if(Frequency.ContainsKey(word))
                {
                    ++Frequency[word];
                }
                //If the word has not been found, add it to the list 
                else
                {
                    Frequency.Add(word, 1);
                }
            }
        }

        public void WriteResults()
        {
            IEnumerable<KeyValuePair<string, int>> orderedFrequency = ReturnOrderedFrequency();

            using (StreamWriter file = new StreamWriter(Path.Combine(configuration.OutputPath, configuration.OutputFile))) 
            {
                file.WriteLine("The top {0} word(s) in the given file are:", configuration.TopWordCount);
                foreach (KeyValuePair<string, int> keyValuePair in orderedFrequency)
                {
                    file.WriteLine(keyValuePair.ToString());
                }
            }
        }

        public IEnumerable<KeyValuePair<string, int>> ReturnOrderedFrequency()
        {
            //Calculate the word frequency if it hasn't been done yet
            if (Frequency.Count == 0)
            {
                CalculateWordFrequency();
            }

            if(configuration.TopWordCount<=0)
            {
                throw new ArgumentException(String.Format("Integer is less than or equal to 0"), "TopWordCount");
            }

            return Frequency.OrderByDescending(word => word.Value).Take(configuration.TopWordCount);
        }
    }
}
