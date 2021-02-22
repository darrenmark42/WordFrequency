using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WordFrequency.Interfaces;

namespace WordFrequency
{
    public class TextPreparation
    {
        IConfiguration _configuration;
        private List<string> stopWords;
        private string text;


        public TextPreparation(IConfiguration configuration)
        {
            _configuration = configuration;
            stopWords = ReadFile(_configuration.StopWordPath, "StopWordPath").Split('\n').ToList();
            text = ReadFile(_configuration.TextPath, "Text Path");
            text = text.ToLower();
        }

        private string ReadFile(string filePath, string argument)
        {
            if(filePath == string.Empty)
            {
                throw new ArgumentException(String.Format("File Path is empty"), argument);
            }

            if(!File.Exists(filePath))
            {
                throw new ArgumentException(String.Format("File does not exist"), argument);
            }

            return File.ReadAllText(filePath);
        }
    }
}
