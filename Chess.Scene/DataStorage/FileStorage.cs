using System.IO;

namespace Chess.Scene.DataStorage
{
    public class FileStorage : IDataStorage
    {
        public string CurrentDirectory { get; set; }

        public string FileName { get; set; }

        public FileStorage(string currentDir, string fileName)
        {
            CurrentDirectory = currentDir;
            FileName = fileName;
        }

        public string Read()
        {
            return File.ReadAllText(Path.Combine(CurrentDirectory, FileName));
        }

        public void Write(string content)
        {
            File.WriteAllText(Path.Combine(CurrentDirectory, FileName), content);
        }
    }
}