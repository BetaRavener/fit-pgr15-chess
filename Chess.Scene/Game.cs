using System;
using System.Collections;
using System.Collections.Generic;

namespace Chess.Scene
{
    public class Game
    {
        public GameState State { get; set; }

        public DateTime? StatedAt { get; set; }

        public DateTime? EndedAt { get; set; }

        public Chessboard Chessboard { get; set; }

        public IList<Figure> FiguresP1 { get; set; }

        public IList<Figure> FiguresP2 { get; set; }


        public Game()
        {
            FiguresP1 = new List<Figure>();
            FiguresP2 = new List<Figure>();
        }

    }
}
