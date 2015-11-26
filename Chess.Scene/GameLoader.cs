using System.IO;
using Newtonsoft.Json;

namespace Chess.Scene
{
    public class GameLoader
    { 

        public string CurrentDirectory { get; set; }

        public GameLoader(string currentDir)
        {
            CurrentDirectory = currentDir;
        }

        public Game LoadGame(string fileName)
        {
            var content = File.ReadAllText(Path.Combine(CurrentDirectory, fileName));

            return JsonConvert.DeserializeObject<Game>(content);
        }

        public void SaveGame(Game game, string fileName)
        {
            var content = JsonConvert.SerializeObject(game);

            File.WriteAllText(Path.Combine(CurrentDirectory, fileName), content);
        }
    }
}