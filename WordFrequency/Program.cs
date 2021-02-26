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
            Configuration configuration = JsonConvert.DeserializeObject<Configuration>(File.ReadAllText("configuration.json"));
            FileSystemChecks fileSystemChecks = new FileSystemChecks();
            fileSystemChecks.FileChecks(configuration.StopWordPath, "StopWordPath");
            fileSystemChecks.FileChecks(configuration.TextPath, "TextPath");
            fileSystemChecks.FolderCheck(configuration.OutputPath, "OutputPath");

            TextPreparation textPreparation = new TextPreparation(configuration);
            PorterStemmer porterStemmer = new PorterStemmer();

            List<string> words = new List<string>();
            List<string> stemmedWords = new List<string>();

            words = textPreparation.ReturnPreparedText();
            foreach(string word in words)
            {
                string temp = porterStemmer.StemWord(word);
                stemmedWords.Add(temp);
            }

            DetermineWordFrequency determineWordFrequency = new DetermineWordFrequency(stemmedWords, configuration);
            determineWordFrequency.CalculateWordFrequency();
            determineWordFrequency.WriteResults();
        }
    }
}
