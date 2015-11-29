using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using OpenTK.Graphics;

namespace Chess.Scene
{
    public class Game
    {
        public GameState State { get; set; }

        public DateTime? StatedAt { get; set; }

        public DateTime? EndedAt { get; set; }

        public Chessboard Chessboard { get; set; }

        public Player Player1 { get; set; }

        public Player Player2 { get; set; }

        public Game()
        {
            State = GameState.NotStarted;
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

            var figures = Player1.Figures.Union(Player2.Figures);
            var sceneObjects = figures.Select(x => (SceneObject) x).ToList();

            if(chessboard != null)
                sceneObjects.Add(chessboard);

            return sceneObjects;
        }

        public void BuildBaseLayout()
        {
            Chessboard = new Chessboard();
            Player1 = new Player() { Color = Color4.White, Name = "Player1"};
            Player2 = new Player() { Color = Color4.Black, Name = "Player2"};


            Player1.CreateFigure(new ChessboardPosition(0, 0), FigureType.Rook);
            Player1.CreateFigure(new ChessboardPosition(1, 0), FigureType.Knight);
            Player1.CreateFigure(new ChessboardPosition(2, 0), FigureType.Bishop);
            Player1.CreateFigure(new ChessboardPosition(3, 0), FigureType.Queen);
            Player1.CreateFigure(new ChessboardPosition(4, 0), FigureType.King);
            Player1.CreateFigure(new ChessboardPosition(5, 0), FigureType.Bishop);
            Player1.CreateFigure(new ChessboardPosition(6, 0), FigureType.Knight);
            Player1.CreateFigure(new ChessboardPosition(7, 0), FigureType.Rook);

            Player2.CreateFigure(new ChessboardPosition(0, 7), FigureType.Rook);
            Player2.CreateFigure(new ChessboardPosition(1, 7), FigureType.Knight);
            Player2.CreateFigure(new ChessboardPosition(2, 7), FigureType.Bishop);
            Player2.CreateFigure(new ChessboardPosition(3, 7), FigureType.King);
            Player2.CreateFigure(new ChessboardPosition(4, 7), FigureType.Queen);
            Player2.CreateFigure(new ChessboardPosition(5, 7), FigureType.Bishop);
            Player2.CreateFigure(new ChessboardPosition(6, 7), FigureType.Knight);
            Player2.CreateFigure(new ChessboardPosition(7, 7), FigureType.Rook);

            for (uint i = 0; i < 8; i++)
            {
                Player1.CreateFigure(new ChessboardPosition(i, 1), FigureType.Pawn);
                Player2.CreateFigure(new ChessboardPosition(i, 6), FigureType.Pawn);

            }
        }

    }
}
