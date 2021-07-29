using System;
using System.Collections.Generic;
using System.Text;

namespace board
{
    abstract class Piece
    {
        public Position Position { get; set; }
        public Color Color { get;protected set; }
        public int MoveQuantities { get;protected set; }
        public Board Board { get; protected set; }

        public Piece(Board board,Color color)
        {
            Position = null;
            Color = color;
            MoveQuantities = 0;
            Board = board;
        }

        public void IncreaseMoveQuantity()
        {
            MoveQuantities++;
        }
        public void DecreaseMoveQuantity()
        {
            MoveQuantities--;
        }
        public bool ExistMoves()
        {
            bool[,] mat = PossibleMoves();

            for(int i = 0; i < Board.Rows; i++)
            {
                for (int c = 0; c < Board.Columns; c++)
                {
                    if (mat[i, c])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool CanMoveToPos(Position pos)
        {
            return PossibleMoves()[pos.Row, pos.Column];
        }
        public abstract bool[,] PossibleMoves();
    }
}
