using board;
using System.Collections.Generic;

namespace chess
{
    class ChessGame
    {
        public Board Board { get; private set; }
        public int Turn { get; private set; }
        public Color Player { get; private set; }
        public bool End { get; private set; }
        private List<Piece> Pieces = new List<Piece>();
        private List<Piece> CapturedPieces = new List<Piece>();

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
            if (capturedPiece != null)
            {
                CapturedPieces.Add(capturedPiece);
            }
        }
        public void RealizeMove(Position origin, Position destin)
        {
            ExecuteMove(origin, destin);
            Turn++;
            ChangePlayer();
        }

        private void ChangePlayer()
        {
            if (Player == Color.White)
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
            if (Board.GetPiece(pos) == null)
            {
                throw new BoardExeption("Piece doesnt exists");
            }
            if (Player != Board.GetPiece(pos).Color)
            {
                throw new BoardExeption("Player is incorrect");
            }
            if (!Board.GetPiece(pos).ExistMoves())
            {
                throw new BoardExeption("There is no moves to this piece");
            }

        }

        public void ValidateDestinPos(Position origin, Position destin)
        {
            if (!Board.GetPiece(origin).CanMoveToPos(destin))
            {
                throw new BoardExeption("Invalid move");
            }

        }
        public List<Piece> AllCapturedPieces(Color color)
        {
            List<Piece> captured = new List<Piece>();
            foreach (Piece piece in CapturedPieces)
            {
                if (piece.Color == color)
                {
                    captured.Add(piece);
                }
            }
            return captured;
        }
        public List<Piece> PiecesInGame(Color color)
        {
            List<Piece> piecesGame = new List<Piece>();
            foreach (Piece piece in Pieces)
            {
                if (piece.Color == color)
                {
                    piecesGame.Add(piece);
                }
            }
            return piecesGame;
        }
        public void PutNewPiece(char column, int row, Piece piece)
        {
            Board.PutPiece(piece, new ChessPosition(column, row).ToPosition());
            Pieces.Add(piece);
        }
        private void PutPieces()
        {

            PutNewPiece('c', 1, new Rook(Board, Color.White));
            PutNewPiece('c', 8, new Rook(Board, Color.Black));

        }
    }
}
