using System;
using board;
using chess;

namespace ChessGame
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                Board board = new Board(8, 8);

                board.PutPiece(new King(board, Color.Black), new Position(0, 0));
                board.PutPiece(new King(board, Color.Black), new Position(2, 1));
                board.PutPiece(new Rook(board, Color.Black), new Position(1, 2));
                board.PutPiece(new Rook(board, Color.Black), new Position(5, 3));
                board.PutPiece(new Rook(board, Color.Black), new Position(0, 3));
                board.PutPiece(new Rook(board, Color.White), new Position(0, 5));

                Screen.ShowBoard(board);
            }catch(BoardExeption err)
            {
                Console.WriteLine(err.Message);
            }

            
        }
    }
}
