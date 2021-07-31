using board;

namespace chess
{
    class Kinight:Piece
    {
        public Kinight(Board board, Color color) : base(board, color) { }

        public override string ToString()
        {
            return "J";
        }
        public bool CanMove(Position pos)
        {
            Piece piece = Board.GetPiece(pos);
            return piece == null || piece.Color != Color;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] mat = new bool[Board.Rows, Board.Columns];
            Position pos = new Position(0, 0);

            pos.DefineValue(Position.Row + 2, Position.Column + 1);
            if (Board.ValidPos(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            pos.DefineValue(Position.Row + 2, Position.Column - 1);
            if (Board.ValidPos(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            pos.DefineValue(Position.Row -1, Position.Column + 2);
            if (Board.ValidPos(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            pos.DefineValue(Position.Row + 1, Position.Column + 2);
            if (Board.ValidPos(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            pos.DefineValue(Position.Row -1, Position.Column - 2);
            if (Board.ValidPos(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            pos.DefineValue(Position.Row + 1, Position.Column -2);
            if (Board.ValidPos(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            pos.DefineValue(Position.Row - 2, Position.Column + 1);
            if (Board.ValidPos(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }
            pos.DefineValue(Position.Row - 2, Position.Column - 1);
            if (Board.ValidPos(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }
            return mat;
        }
    }
}
