using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using WordFrequency.Models;

namespace WordFrequency
{
    class Program
    {
        static void Main(string[] args)
        {
            //Read in configuration file
            Configuration configuration = JsonConvert.DeserializeObject<Configuration>(File.ReadAllText("configuration.json"));
            //Verify files and folders exist 
            FileSystemChecks fileSystemChecks = new FileSystemChecks();
            fileSystemChecks.FileChecks(configuration.StopWordPath, "StopWordPath");
            fileSystemChecks.FileChecks(configuration.TextPath, "TextPath");
            fileSystemChecks.FolderCheck(configuration.OutputPath, "OutputPath");

            TextPreparation textPreparation = new TextPreparation(configuration);
            PorterStemmer porterStemmer = new PorterStemmer();

            List<string> words = new List<string>();
            List<string> stemmedWords = new List<string>();

            //Remove stopwords and non-alphanumeric characters
            words = textPreparation.ReturnPreparedText();
            //Stem each word
            foreach(string word in words)
            {
                string temp = porterStemmer.StemWord(word);
                stemmedWords.Add(temp);
            }

            //Calculate word frequency and create report
            DetermineWordFrequency determineWordFrequency = new DetermineWordFrequency(stemmedWords, configuration);
            determineWordFrequency.CalculateWordFrequency();
            determineWordFrequency.WriteResults();
        }
    }
}
