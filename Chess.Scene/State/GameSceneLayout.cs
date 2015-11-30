using RayTracer;

namespace Chess.Scene.State
{
    public class GameSceneLayout : Game
    {
        public Camera Camera { get; set; }
        
        public LightSource Light { get; set; }

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
    }
}