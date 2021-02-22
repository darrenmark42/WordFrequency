using WordFrequency.Interfaces;

namespace WordFrequency.Models
{
    public class Configuration : IConfiguration
    {
        public Configuration(string stopWordPath, string textPath)
        {
            StopWordPath = stopWordPath;
            TextPath = textPath;
        }

        public string StopWordPath { get; set; }

        public string TextPath { get; set; }
    }
}
