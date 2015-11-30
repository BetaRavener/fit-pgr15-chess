namespace Chess.Scene.DataStorage
{
    public interface IDataStorage
    {
        string Read();
        void Write(string content);
    }
}