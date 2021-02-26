using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WordFrequency.Interfaces;

namespace WordFrequency
{
    public class TextPreparation
    {
        private IConfiguration _configuration;
        public List<string> stopWords { get; set; }
        public List<string> text { get; set; }


        public TextPreparation(IConfiguration configuration)
        {
            _configuration = configuration;
            stopWords = File.ReadAllText(_configuration.StopWordPath).Split('\n').ToList();
            stopWords = stopWords.ConvertAll(x => x.ToLower());
            text = new List<string>();
            string line = String.Empty;
            StreamReader file = new StreamReader(_configuration.TextPath);
            //Read in the text file line by line
            while((line = file.ReadLine()) != null)
            {
                //Break up each line by the word 
                List<string> temp = line.Split(' ').ToList();
                foreach(string word in temp)
                {
                    text.Add(word);
                }
            }

            //Set the text to have the same casing 
            text = text.ConvertAll(x => x.ToLower());
        }

        //Added to make testing easier 
        public TextPreparation()
        {
        }


        public void RemoveStopWords()
        {
            foreach (string word in stopWords)
            {
                if(word != string.Empty || word != " ")
                {
                    text.RemoveAll(x => x == word);
                }    
            }
        }

        public void ReturnAlphanumericCharacters()
        {
            
            for(int i=0; i<text.Count;++i)
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach(char c in text[i])
                {
                    if (Char.IsLetterOrDigit(c))
                    {
                        stringBuilder.Append(c);
                    }
                }

                //Stringbuilder will be empty if word is only non-alphanumeric characters
                if(stringBuilder.Length>0)
                {
                    //Update the word with only the alphanumeric characters
                    text[i] = stringBuilder.ToString();
                }
                else
                {
                    //Set to empty so they can be removed at the end
                    text[i] = string.Empty;
                }
            }

            //Remove any entries that ended up being empty strings are removing all non-alphanumeric characters
            text.RemoveAll(x => x == string.Empty);
        }

        public List<string> ReturnPreparedText()
        {
            RemoveStopWords();
            ReturnAlphanumericCharacters();

            return text;
        }
    }
}
