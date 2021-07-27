using board;

namespace chess
{
    class Rook : Piece
    {
        public Rook(Board board, Color color) : base(board,color)
        {

        }
        public override string ToString()
        {
            return "R";
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

            // acima
            pos.DefineValue(Position.Row - 1, Position.Column);
            while (Board.ValidPos(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != Color)
                {
                    break;
                }
                pos.Row = pos.Row - 1;
            }

            // abaixo
            pos.DefineValue(Position.Row + 1, Position.Column);
            while (Board.ValidPos(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != Color)
                {
                    break;
                }
                pos.Row = pos.Row + 1;
            }

            // direita
            pos.DefineValue(Position.Row, Position.Column + 1);
            while (Board.ValidPos(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != Color)
                {
                    break;
                }
                pos.Column = pos.Column + 1;
            }

            // esquerda
            pos.DefineValue(Position.Row, Position.Column - 1);
            while (Board.ValidPos(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != Color)
                {
                    break;
                }
                pos.Column = pos.Column - 1;
            }

            return mat;
        }
    }
}
