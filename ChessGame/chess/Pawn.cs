using board;
namespace chess
{
    class Pawn : Piece
    {
        public Pawn(Board board, Color color) : base(board, color) { }

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
                pos.DefineValue(Position.Row - 1, Position.Column -1);
                if (Board.ValidPos(pos) && HasEnemy(pos))
                {
                    mat[pos.Row, pos.Column] = true;
                }
                pos.DefineValue(Position.Row - 1, Position.Column +1);
                if (Board.ValidPos(pos) && HasEnemy(pos))
                {
                    mat[pos.Row, pos.Column] = true;
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
                if (Board.ValidPos(pos) &&  HasEnemy(pos))
                {
                    mat[pos.Row, pos.Column] = true;
                }
                pos.DefineValue(Position.Row + 1, Position.Column - 1);
                if (Board.ValidPos(pos) &&  HasEnemy(pos))
                {
                    mat[pos.Row, pos.Column] = true;
                }
            }
            return mat;     
        }
    }
}
