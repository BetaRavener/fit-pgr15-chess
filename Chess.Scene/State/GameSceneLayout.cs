using RayTracer;

namespace Chess.Scene.State
{
    public class GameSceneLayout : Game
    {
        public GameSceneLayout()
        {
            Camera = new Camera();
            Light = new LightSource();
        }

        public GameSceneLayout(Camera camera, LightSource light)
        {
            Camera = camera;
            Light = light;
        }

        public Camera Camera { get; set; }

        public LightSource Light { get; set; }
    }
}