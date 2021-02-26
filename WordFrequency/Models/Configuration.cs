using WordFrequency.Interfaces;

namespace WordFrequency.Models
{
    public class Configuration : IConfiguration
    {
        public Configuration()
        {
        }

        public string StopWordPath { get; set; }

        public string TextPath { get; set; }

        public string OutputPath { get; set; }

        public string OutputFile { get; set; }

        public int TopWordCount { get; set; }
    }
}
