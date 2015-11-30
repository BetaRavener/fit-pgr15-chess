using Chess.Scene.State;

namespace Chess.Scene
{
    public interface IStateLoader
    {
        void SaveState(Game game);

        Game LoadState();
    }
}