using System.IO;

namespace Chess.Scene.DataStorage
{
    public class FileStorage : IDataStorage
    {
        public FileStorage(string currentDir, string fileName)
        {
            FilePath = Path.Combine(currentDir, fileName);
        }

        public FileStorage(string filePath)
        {
            FilePath = filePath;
        }

        public string CurrentDirectory { get; set; }

        public string FileName { get; set; }

        public string FilePath { get; set; }

        public string Read()
        {
            return File.ReadAllText(FilePath);
        }

        public void Write(string content)
        {
            File.WriteAllText(FilePath, content);
        }
    }
}