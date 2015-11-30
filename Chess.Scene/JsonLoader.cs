using System.Security.Cryptography.X509Certificates;
using Chess.Scene.DataStorage;
using Chess.Scene.State;
using Chess.Scene.State.JsonConvertor;
using Newtonsoft.Json;

namespace Chess.Scene
{
    public class JsonLoader<T> 
        where T : Game
    { 
        public IDataStorage DataStorage { get; set; }

        public JsonLoader(IDataStorage dataStorage)
        {
            DataStorage = dataStorage;
        }

        public T LoadGame()
        {
            var content = DataStorage.Read();

            return JsonConvert.DeserializeObject<T>(content, new VectorConverter());
        }

        public void SaveGame(T game)
        {
            var settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            };
            settings.Converters.Add(new VectorConverter());

            var content = JsonConvert.SerializeObject(game, settings);

            DataStorage.Write(content);
        }
    }
}