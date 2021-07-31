using board;

namespace chess
{
    class Bisp : Piece
    {
        public Bisp(Board board, Color color) : base(board, color)
        { }

        public override string ToString()
        {
            return "B";
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

            pos.DefineValue(Position.Row - 1, Position.Column - 1);
            while (Board.ValidPos(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != Color)
                {
                    break;
                }
                pos.DefineValue(pos.Row - 1, pos.Column - 1);
            }

            pos.DefineValue(Position.Row + 1, Position.Column + 1);
            while (Board.ValidPos(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != Color)
                {
                    break;
                }
                pos.DefineValue(pos.Row + 1, pos.Column + 1);
            }

            pos.DefineValue(Position.Row - 1, Position.Column + 1);
            while (Board.ValidPos(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != Color)
                {
                    break;
                }
                pos.DefineValue(pos.Row - 1, pos.Column + 1);
            }

            pos.DefineValue(Position.Row + 1, Position.Column - 1);
            while (Board.ValidPos(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != Color)
                {
                    break;
                }
                pos.DefineValue(pos.Row + 1, pos.Column - 1);
            }
            return mat;
        }
    }
}
