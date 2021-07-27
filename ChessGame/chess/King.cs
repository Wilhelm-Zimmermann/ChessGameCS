using board;

namespace chess
{
    class King : Piece
    {
        public King(Board board, Color color) : base(board, color)
        {

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
            return mat;
        }
    }
}
