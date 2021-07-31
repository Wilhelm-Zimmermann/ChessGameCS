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
        private HashSet<Piece> Pieces = new HashSet<Piece>();
        private HashSet<Piece> CapturedPieces = new HashSet<Piece>();
        public bool Xeque { get; private set; }
        public Piece EnPassant { get; private set; }

        public ChessGame()
        {
            Board = new Board(8, 8);
            Turn = 1;
            Player = Color.White;
            End = false;
            Xeque = false;
            EnPassant = null;
            PutPieces();
        }
        public Piece ExecuteMove(Position origin, Position destin)
        {
            Piece p = Board.RemovePiece(origin);
            p.IncreaseMoveQuantity();
            Piece capturedPiece = Board.RemovePiece(destin);
            Board.PutPiece(p, destin);
            if (capturedPiece != null)
            {
                CapturedPieces.Add(capturedPiece);
            }
            // #Roque
            if(p is King && destin.Column == origin.Column + 2)
            {
                Position rookOrigin = new Position(origin.Row, origin.Column + 3);
                Position rookDestin = new Position(origin.Row, origin.Column + 1);
                Piece rook = Board.RemovePiece(rookOrigin);
                rook.IncreaseMoveQuantity();
                Board.PutPiece(rook, rookDestin);
            }
            if (p is King && destin.Column == origin.Column - 2)
            {
                Position rookOrigin = new Position(origin.Row, origin.Column - 4);
                Position rookDestin = new Position(origin.Row, origin.Column - 1);
                Piece rook = Board.RemovePiece(rookOrigin);
                rook.IncreaseMoveQuantity();
                Board.PutPiece(rook, rookDestin);
            }
            // # En Passant
            if(p is Pawn)
            {
                if(origin.Column != destin.Column && capturedPiece == null)
                {
                    Position posP;
                    if(p.Color == Color.White)
                    {
                        posP = new Position(destin.Row + 1, destin.Column);

                    }
                    else
                    {
                        posP = new Position(destin.Row - 1, destin.Column);
                    }
                    capturedPiece = Board.RemovePiece(posP);
                    CapturedPieces.Add(capturedPiece); 
                }
            }
            return capturedPiece;
        }
        public void RealizeMove(Position origin, Position destin)
        {
            Piece capturedPiece = ExecuteMove(origin, destin);
            if (Xeq(Player))
            {
                UndoMove(origin, destin, capturedPiece);
                throw new BoardExeption("You cannot put yourself in xeq");
            }
            Piece piece = Board.GetPiece(destin);
            // # Promotion
            if(piece is Pawn)
            {
                if((piece.Color == Color.White && destin.Row == 0) || (piece.Color == Color.Black && destin.Row == 7))
                {
                    piece = Board.RemovePiece(destin);
                    Pieces.Remove(piece);
                    Piece queen = new Queen(Board, piece.Color);
                    Board.PutPiece(queen, destin);
                    Pieces.Add(queen);
                }
            }

            if (Xeq(Enemy(Player)))
            {
                Xeque = true;
            }
            else
            {
                Xeque = false;
            }
            if (TestMate(Enemy(Player)))
            {
                End = true;
            }
            else
            {
                Turn++;
                ChangePlayer();
            }
            // #En passant
            if(piece is Pawn && (destin.Row == origin.Row -2 || destin.Row == origin.Row +2 ))
            {
                EnPassant = piece;
            }
            else
            {
                EnPassant = null;   
            }
        }
        public void UndoMove(Position origin, Position destin, Piece capturedPiece)
        {
            Piece p = Board.RemovePiece(destin);
            p.DecreaseMoveQuantity();
            if (capturedPiece != null)
            {
                Board.PutPiece(capturedPiece, destin);
                CapturedPieces.Remove(capturedPiece);
            }
            if (p is King && destin.Column == origin.Column + 2)
            {
                Position rookOrigin = new Position(origin.Row, origin.Column + 3);
                Position rookDestin = new Position(origin.Row, origin.Column + 1);
                Piece rook = Board.RemovePiece(rookOrigin);
                rook.DecreaseMoveQuantity();
                Board.PutPiece(rook, rookOrigin);
            }
            if (p is King && destin.Column == origin.Column - 2)
            {
                Position rookOrigin = new Position(origin.Row, origin.Column - 4);
                Position rookDestin = new Position(origin.Row, origin.Column - 1);
                Piece rook = Board.RemovePiece(rookOrigin);
                rook.DecreaseMoveQuantity();
                Board.PutPiece(rook, rookOrigin);
            }
            // #Enpassant
            if (p is Pawn)
            {
                if (origin.Column != destin.Column && capturedPiece == EnPassant)
                {
                    Piece pawn = Board.RemovePiece(destin);
                    Position posP;
                    if(pawn.Color == Color.White)
                    {
                        posP = new Position(3, destin.Column);
                    }
                    else
                    {
                        posP = new Position(4, destin.Column);
                    }
                    Board.PutPiece(pawn,posP);
                }
            }

            Board.PutPiece(p, origin);

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

        private Piece King(Color color)
        {
            foreach (Piece x in PiecesInGame(color))
            {
                if (x is King)
                {
                    return x;
                }
            }
            return null;
        }

        public bool Xeq(Color color)
        {
            Piece king = King(color);
            if (king == null)
            {
                End = true;
            }
            foreach (Piece p in PiecesInGame(Enemy(color)))
            {
                bool[,] mat = p.PossibleMoves();
                if (mat[king.Position.Row, king.Position.Column])
                {
                    return true;
                }

            }
            return false;
        }

        public bool TestMate(Color color)
        {
            if (!Xeq(color))
            {
                return false;
            }
            foreach (Piece piece in PiecesInGame(color))
            {
                bool[,] mat = piece.PossibleMoves();
                for (int i = 0; i < Board.Rows; i++)
                {
                    for (int c = 0; c < Board.Columns; c++)
                    {
                        if (mat[i, c])
                        {
                            Position origin = piece.Position;
                            Position destin = new Position(i, c);
                            Piece capturedPiece = ExecuteMove(origin, destin);
                            bool testXeq = Xeq(color);
                            UndoMove(origin, destin, capturedPiece);
                            if (!testXeq)
                            {
                                return false;
                            }

                        }
                    }
                }
            }
            return true;
        }
        private Color Enemy(Color color)
        {
            if (color == Color.White)
            {
                return Color.Black;
            }
            else
            {
                return Color.White;
            }
        }
        public HashSet<Piece> AllCapturedPieces(Color color)
        {
            HashSet<Piece> captured = new HashSet<Piece>();
            foreach (Piece piece in CapturedPieces)
            {
                if (piece.Color == color)
                {
                    captured.Add(piece);
                }
            }
            return captured;
        }
        public HashSet<Piece> PiecesInGame(Color color)
        {
            HashSet<Piece> piecesGame = new HashSet<Piece>();
            foreach (Piece piece in Pieces)
            {
                if (piece.Color == color)
                {
                    piecesGame.Add(piece);
                }
            }
            piecesGame.ExceptWith(AllCapturedPieces(color));
            return piecesGame;
        }
        public void PutNewPiece(char column, int row, Piece piece)
        {
            Board.PutPiece(piece, new ChessPosition(column, row).ToPosition());
            Pieces.Add(piece);
        }
        private void PutPieces()
        {
            // White
            
            PutNewPiece('a', 1, new Rook(Board, Color.White));
            PutNewPiece('a', 2, new Pawn(Board, Color.White,this));
            PutNewPiece('b', 1, new Kinight(Board, Color.White));
            PutNewPiece('b', 2, new Pawn(Board, Color.White,this));
            PutNewPiece('c', 1, new Bisp(Board, Color.White));
            PutNewPiece('c', 2, new Pawn(Board, Color.White,this));
            PutNewPiece('d', 1, new Queen(Board, Color.White));
            PutNewPiece('d', 2, new Pawn(Board, Color.White,this));
            PutNewPiece('e', 1, new King(Board, Color.White,this));
            PutNewPiece('e', 2, new Pawn(Board, Color.White,this));
            PutNewPiece('f', 1, new Bisp(Board, Color.White));
            PutNewPiece('f', 2, new Pawn(Board, Color.White,this));
            PutNewPiece('g', 1, new Kinight(Board, Color.White));
            PutNewPiece('g', 2, new Pawn(Board, Color.White,this));
            PutNewPiece('h', 1, new Rook(Board, Color.White));
            PutNewPiece('h', 2, new Pawn(Board, Color.White,this));

            // Black

            PutNewPiece('a', 8, new Rook(Board, Color.Black));
            PutNewPiece('a', 7, new Pawn(Board, Color.Black,this));
            PutNewPiece('b', 8, new Bisp(Board, Color.Black));
            PutNewPiece('b', 7, new Pawn(Board, Color.Black,this));
            PutNewPiece('c', 8, new Kinight(Board, Color.Black));
            PutNewPiece('c', 7, new Pawn(Board, Color.Black,this));
            PutNewPiece('d', 8, new Queen(Board, Color.Black));
            PutNewPiece('d', 7, new Pawn(Board, Color.Black,this));
            PutNewPiece('e', 8, new King(Board, Color.Black,this));
            PutNewPiece('e', 7, new Pawn(Board, Color.Black,this));
            PutNewPiece('f', 8, new Bisp(Board, Color.Black));
            PutNewPiece('f', 7, new Pawn(Board, Color.Black,this));
            PutNewPiece('g', 8, new Kinight(Board, Color.Black));
            PutNewPiece('g', 7, new Pawn(Board, Color.Black,this));
            PutNewPiece('h', 8, new Rook(Board, Color.Black));
            PutNewPiece('h', 7, new Pawn(Board, Color.Black,this));
            

        }
    }
}
