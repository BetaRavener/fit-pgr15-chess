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

            return JsonConvert.DeserializeObject<T>(content, CreateSettings());
        }

        public void SaveGame(T game)
        {
     

            var content = JsonConvert.SerializeObject(game, CreateSettings());

            DataStorage.Write(content);
        }

        private JsonSerializerSettings CreateSettings()
        {
            var settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                TypeNameHandling = TypeNameHandling.Objects
            };
            settings.Converters.Add(new VectorConverter());

            return settings;
        }
    }
}