using board;
using chess;
namespace chess
{
    class Pawn : Piece
    {
        private ChessGame Game;
        public Pawn(Board board, Color color, ChessGame game) : base(board, color)
        {
            Game = game;
        }

        public override string ToString()
        {
            return "P";
        }

        private bool HasEnemy(Position pos)
        {
            Piece piece = Board.GetPiece(pos);
            return piece != null && piece.Color != Color;
        }

        private bool Free(Position pos)
        {
            return Board.GetPiece(pos) == null;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] mat = new bool[Board.Rows, Board.Columns];

            Position pos = new Position(0, 0);

            if (Color == Color.White)
            {
                pos.DefineValue(Position.Row - 1, Position.Column);
                if (Board.ValidPos(pos) && Free(pos))
                {
                    mat[pos.Row, pos.Column] = true;
                }
                pos.DefineValue(Position.Row - 2, Position.Column);
                if (Board.ValidPos(pos) && Free(pos) && MoveQuantities == 0)
                {
                    mat[pos.Row, pos.Column] = true;
                }
                pos.DefineValue(Position.Row - 1, Position.Column - 1);
                if (Board.ValidPos(pos) && HasEnemy(pos))
                {
                    mat[pos.Row, pos.Column] = true;
                }
                pos.DefineValue(Position.Row - 1, Position.Column + 1);
                if (Board.ValidPos(pos) && HasEnemy(pos))
                {
                    mat[pos.Row, pos.Column] = true;
                }
                // # En passant
                if (Position.Row == 3)
                {
                    Position left = new Position(Position.Row, Position.Column - 1);
                    if (Board.ValidPos(left) && HasEnemy(left) && Board.GetPiece(left) == Game.EnPassant)
                    {
                        mat[left.Row - 1, left.Column] = true;
                    }
                    Position right = new Position(Position.Row, Position.Column + 1);
                    if (Board.ValidPos(right) && HasEnemy(right) && Board.GetPiece(right) == Game.EnPassant)
                    {
                        mat[right.Row - 1, right.Column] = true;
                    }
                }
            }
            else
            {
                pos.DefineValue(Position.Row + 1, Position.Column);
                if (Board.ValidPos(pos) && Free(pos))
                {
                    mat[pos.Row, pos.Column] = true;
                }
                pos.DefineValue(Position.Row + 2, Position.Column);
                if (Board.ValidPos(pos) && Free(pos) && MoveQuantities == 0)
                {
                    mat[pos.Row, pos.Column] = true;
                }
                pos.DefineValue(Position.Row + 1, Position.Column + 1);
                if (Board.ValidPos(pos) && HasEnemy(pos))
                {
                    mat[pos.Row, pos.Column] = true;
                }
                pos.DefineValue(Position.Row + 1, Position.Column - 1);
                if (Board.ValidPos(pos) && HasEnemy(pos))
                {
                    mat[pos.Row, pos.Column] = true;
                }

                if (Position.Row == 4)
                {
                    Position left = new Position(Position.Row, Position.Column - 1);
                    if (Board.ValidPos(left) && HasEnemy(left) && Board.GetPiece(left) == Game.EnPassant)
                    {
                        mat[left.Row + 1, left.Column] = true;
                    }
                    Position right = new Position(Position.Row, Position.Column + 1);
                    if (Board.ValidPos(right) && HasEnemy(right) && Board.GetPiece(right) == Game.EnPassant)
                    {
                        mat[right.Row + 1, right.Column] = true;
                    }
                }

            }
            return mat;
        }
    }
}
