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
            stopWords = ReadFile(_configuration.StopWordPath, "StopWordPath").Split('\n').ToList();
            stopWords = stopWords.ConvertAll(x => x.ToLower());
            text = ReadFile(_configuration.TextPath, "Text Path").Split(' ').ToList();
            text = text.ConvertAll(x => x.ToLower());
        }

        public TextPreparation()
        {
        }

        private string ReadFile(string filePath, string argument)
        {
            if(filePath == string.Empty)
            {
                throw new ArgumentException(String.Format("File Path is empty"), argument);
            }

            else if(!File.Exists(filePath))
            {
                throw new ArgumentException(String.Format("File does not exist"), argument);
            }
            //Checks for an empty file
            else if(new FileInfo(filePath).Length==0)
            {
                throw new ArgumentException(String.Format("File is empty"), argument);
            }
            else 
            {
                return File.ReadAllText(filePath);
            }
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

                //String will be empty if word is only non-alphanumeric characters
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
