namespace WordFrequency.Interfaces
{
    public interface IFileSystemChecks
    {
        public void FileChecks(string filePath, string parameter);
        public void FolderCheck(string folderPath, string parameter);
    }
}
