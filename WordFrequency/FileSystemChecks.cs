using System;
using System.IO;
using WordFrequency.Interfaces;

namespace WordFrequency
{
    public class FileSystemChecks : IFileSystemChecks
    {
        public FileSystemChecks()
        {

        }

        public void FileChecks(string filePath, string parameter)
        {
            if (filePath == string.Empty)
            {
                throw new ArgumentException(String.Format("File Path is empty"), parameter);
            }

            if (!File.Exists(filePath))
            {
                throw new ArgumentException(String.Format("File does not exist"), parameter);
            }
            //Checks for an empty file
            if (new FileInfo(filePath).Length == 0)
            {
                throw new ArgumentException(String.Format("File is empty"), parameter);
            }
        }

        public void FolderCheck(string folderPath, string parameter)
        {
            if(folderPath == string.Empty)
            {
                throw new ArgumentException(String.Format("Folder Path is empty"), parameter);
            }

            if(!Directory.Exists(folderPath))
            {
                throw new ArgumentException(String.Format("Folder Path does not exist"), parameter);
            }
        }
    }
}
