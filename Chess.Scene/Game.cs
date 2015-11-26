using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Chess.Scene
{
    public class Game
    {
        public GameState State { get; set; }

        public DateTime? StatedAt { get; set; }

        public DateTime? EndedAt { get; set; }

        public Chessboard Chessboard { get; set; }

        public IList<Figure> Figures { get; set; }


        public Game()
        {
            State = GameState.NotStarted;
            Figures = new List<Figure>();
        }

        public void Start()
        {
            if(State != GameState.NotStarted)
                throw new Exception("Game already started or already ended.");

            StatedAt = DateTime.Now;
            State = GameState.Running;
        }

        public void End()
        {
            if(State != GameState.Running)
                throw new Exception("Game not started, so it can't be ended.");

            EndedAt = DateTime.Now;
            State = GameState.Ended;
        }

        public IList<SceneObject> GetSceneObjects()
        {
            var chessboard = (SceneObject) Chessboard;
            var sceneObjects = Figures.Select(x => (SceneObject) x).ToList();

            if(chessboard != null)
                sceneObjects.Add(chessboard);

            return sceneObjects;
        }

    }
}
