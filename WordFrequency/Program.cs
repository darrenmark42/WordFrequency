using Newtonsoft.Json;
using System;
using System.IO;
using WordFrequency.Models;

namespace WordFrequency
{
    class Program
    {
        static void Main(string[] args)
        {
            Configuration configuration = JsonConvert.DeserializeObject<Configuration>(File.ReadAllText("configuration.json"));

            TextPreparation textPreparation = new TextPreparation(configuration);

            Console.WriteLine("Hello World!");
        }
    }
}
