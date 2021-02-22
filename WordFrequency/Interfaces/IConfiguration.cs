namespace WordFrequency.Interfaces
{
    public interface IConfiguration
    {
        public string StopWordPath { get; }
        public string TextPath { get; }
    }
}
