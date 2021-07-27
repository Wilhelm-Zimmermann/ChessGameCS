using board;

namespace chess
{
    class ChessGame
    {
        public Board Board { get; private set; }
        public int Turn { get; private set; }
        public Color Player { get; private set; }
        public bool End { get; private set; }

        public ChessGame()
        {
            Board = new Board(8, 8);
            Turn = 1;
            Player = Color.White;
            End = false;
            PutPieces();
        }
        public void ExecuteMove(Position origin, Position destin)
        {
            Piece p = Board.RemovePiece(origin);
            p.IncreaseMoveQuantity();
            Piece capturedPiece = Board.RemovePiece(destin);
            Board.PutPiece(p, destin);
        }
        public void RealizeMove(Position origin, Position destin)
        {
            ExecuteMove(origin, destin);
            Turn++;
            ChangePlayer();
        }

        private void ChangePlayer()
        {
            if(Player == Color.White)
            {
                Player = Color.Black;
            }
            else
            {
                Player = Color.White;
            }
        }

        public void ValidateOriginPos(Position pos)
        {
            if(Board.GetPiece(pos) == null)
            {
                throw new BoardExeption("Piece doesnt exists");
            }
            if(Player != Board.GetPiece(pos).Color)
            {
                throw new BoardExeption("Player is incorrect");
            }
            if (!Board.GetPiece(pos).ExistMoves())
            {
                throw new BoardExeption("There is no moves to this piece");
            }
            
        }

        public void ValidateDestinPos(Position origin,Position destin)
        {
            if (!Board.GetPiece(origin).CanMoveToPos(destin))
            {
                throw new BoardExeption("Invalid move");
            }
            
        }
        private void PutPieces()
        {

            Board.PutPiece(new King(Board, Color.White), new ChessPosition('c',1).ToPosition());
            Board.PutPiece(new Rook(Board, Color.White), new ChessPosition('d',1).ToPosition());
            Board.PutPiece(new Rook(Board, Color.White), new ChessPosition('d',4).ToPosition());
            Board.PutPiece(new Rook(Board, Color.Black), new ChessPosition('g',4).ToPosition());

        }
    }
}
