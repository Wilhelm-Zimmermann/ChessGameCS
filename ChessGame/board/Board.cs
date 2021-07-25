namespace board
{
    class Board
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        private Piece[,] Pieces;

        public Board(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            Pieces = new Piece[rows, columns];
        }

        public Piece GetPiece(int row, int column)
        {
            return Pieces[row, column];
        }
        public Piece GetPiece(Position pos)
        {
            return Pieces[pos.Row, pos.Column];
        }

        public bool PieceExists(Position pos)
        {
            ValidatePos(pos);
            return GetPiece(pos) != null;
        }
        public void PutPiece(Piece p, Position pos)
        {
            if (PieceExists(pos))
            {
                throw new BoardExeption("Error: There is a piece on this position.....");
            }
            Pieces[pos.Row, pos.Column] = p;
            p.Position = pos;
        }

        public Piece RemovePiece(Position pos)
        {
            if(GetPiece(pos) == null)
            {
                return null;
            }
            Piece aux = GetPiece(pos);
            aux.Position = null;
            Pieces[pos.Row, pos.Column] = null;
            return aux;
        }
        public bool ValidPos(Position pos)
        {
            if(pos.Row < 0 || pos.Row >= Rows || pos.Column >= Columns || pos.Column < 0)
            {
                return false;
            }
            return true;
        }
        public void ValidatePos(Position pos)
        {
            if (!ValidPos(pos))
            {
                throw new BoardExeption("Invalid Position");
            }
        }
    }
}
