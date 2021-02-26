namespace WordFrequency.Interfaces
{
    public interface IConfiguration
    {
        public string StopWordPath { get; }
        public string TextPath { get; }
        public string OutputPath { get; }
        public string OutputFile { get; }
        public int TopWordCount { get; }
    }
}
