using board;

namespace chess
{
    class ChessGame
    {
        public Board Board { get; private set; }
        private int Turn;
        private Color Player;
        public bool End { get; private set; }

        public ChessGame()
        {
            Board = new Board(8, 8);
            Turn = 1;
            Player = Color.White;
            End = false;
            PutPieces();
        }
        /*public ChessGame(Board board, int turn, Color player)
        {
            Board = board;
            Turn = turn;
            Player = player;
        }*/

        public void ExecuteMove(Position origin, Position destin)
        {
            Piece p = Board.RemovePiece(origin);
            p.IncreaseMoveQuantity();
            Piece capturedPiece = Board.RemovePiece(destin);
            Board.PutPiece(p, destin);
        }

        private void PutPieces()
        {
            Board.PutPiece(new King(Board, Color.Black), new ChessPosition('c', 1).ToPosition());

        }
    }
}
