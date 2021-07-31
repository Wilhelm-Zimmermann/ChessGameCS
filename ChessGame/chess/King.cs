using board;
using chess;
namespace chess
{
    class King : Piece
    {

        private ChessGame Game;
        public King(Board board, Color color,ChessGame game) : base(board, color)
        {
            Game = game;
        }
        public override string ToString()
        {
            return "K";
        }

        private bool CanMove(Position pos)
        {
            Piece p = Board.GetPiece(pos);
            return p == null || p.Color != Color;
        }

        private bool TestRookToRoque(Position pos)
        {
            Piece piece = Board.GetPiece(pos);
            return piece != null && piece is Rook && piece.Color == Color && piece.MoveQuantities == 0;
        }
        
        public override bool[,] PossibleMoves()
        {
            bool[,] mat = new bool[Board.Rows, Board.Columns];
            Position pos = new Position(0,0);
            // Up
            pos.DefineValue(Position.Row - 1, Position.Column);
            if (Board.ValidPos(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }
            // Diagonal + Up +  Right
            pos.DefineValue(Position.Row - 1, Position.Column + 1);
            if (Board.ValidPos(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }
            // Right
            pos.DefineValue(Position.Row, Position.Column + 1);
            if (Board.ValidPos(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }
            // Diagonal + Down + right
            pos.DefineValue(Position.Row + 1, Position.Column + 1);
            if (Board.ValidPos(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }
            // Down
            pos.DefineValue(Position.Row + 1, Position.Column);
            if (Board.ValidPos(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }
            // Diagonal + Down + left
            pos.DefineValue(Position.Row + 1, Position.Column - 1);
            if (Board.ValidPos(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }
            // Left
            pos.DefineValue(Position.Row, Position.Column - 1);
            if (Board.ValidPos(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }
            // Diagonal + Left + Up
            pos.DefineValue(Position.Row - 1, Position.Column - 1);
            if (Board.ValidPos(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            // # Roque
            if(MoveQuantities == 0 && !Game.Xeque)
            {
                // # Little Roque
                Position rookPos = new Position(Position.Row, Position.Column + 3);
                if (TestRookToRoque(rookPos)) {
                    Position p1 = new Position(Position.Row, Position.Column + 1);
                    Position p2 = new Position(Position.Row, Position.Column + 2);
                    if(Board.GetPiece(p1) == null && Board.GetPiece(p2) == null)
                    {
                        mat[Position.Row, Position.Column +2] = true;
                    }
                }
                Position rookPos2 = new Position(Position.Row, Position.Column - 4);
                if (TestRookToRoque(rookPos2))
                {
                    Position p1 = new Position(Position.Row, Position.Column - 1);
                    Position p2 = new Position(Position.Row, Position.Column - 2);
                    Position p3 = new Position(Position.Row, Position.Column - 3);
                    if (Board.GetPiece(p1) == null && Board.GetPiece(p2) == null && Board.GetPiece(p3) == null)
                    {
                        mat[Position.Row, Position.Column - 2] = true;
                    }
                }
            }
            return mat;
        }
    }
}
