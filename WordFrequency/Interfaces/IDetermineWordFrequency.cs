using System;
using System.Collections.Generic;
using System.Text;

namespace WordFrequency.Interfaces
{
    interface IDetermineWordFrequency
    {
        public Dictionary<string, int> Frequency { get; }
        public List<string> WordList { get; }
        public IConfiguration configuration { get; }
        void CalculateWordFrequency();
        void WriteResults();
    }
}
