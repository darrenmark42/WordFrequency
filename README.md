# WordFrequency

This program computes the frequency of each term in a given text. It does the following:

1. Remove every word in the "stop list" word (words that need to be removed from the text)
2. Removes all non-alphanumeric characters
3. Runs the Porter Stemmer algorithm to determine the morphological root of each remaining word. More on the Porter Stemmer algorithm can be found here:
   https://tartarus.org/martin/PorterStemmer/
4. Calculates the word frequency 

## Requirements

.NET Core 3.1 SDK is required for building and running this program. This has been tested on both Windows 10 and Xubuntu 20.04. 

## Build Instructions
Open the folder with the source code and run the following command:

<em>dotnet build</em>

## Configuration

A configuration file is required to be in the same folder as the WordFrequency.dll. The configuration file has the following values:

  "StopWordPath": - The file path to the stopword list
  "TextPath": - The file path with the text
  "OutputPath": - the folder path where the results will be written to 
  "OutputFile": - The file name for the results file
  "TopWordCount" - The top number of words you want returned 


## Running the program

Run the following command:

<em> dotnet WordFrequency.dll </em>

## Notes 

I've included some sample data files in the "Sample Data" folder. I've also included my results for the sample files in the "Results" folder. 