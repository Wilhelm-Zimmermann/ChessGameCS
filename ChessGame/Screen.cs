using board;
using System;
namespace ChessGame
{
    class Screen
    {
        public static void ShowBoard(Board board)
        {
            for (int i = 0; i < board.Rows; i++)
            {
                Console.Write(8 - i + " ");
                for (int c = 0; c < board.Columns; c++)
                {
                    if(board.GetPiece(i,c) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {

                        PieceShow(board.GetPiece(i, c));
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }
        public static void PieceShow(Piece piece)
        {
            if(piece.Color == Color.White)
            {
                Console.Write(piece);
            }
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;

                Console.Write(piece);
                Console.ForegroundColor = aux;
            }
        }
    }
}
