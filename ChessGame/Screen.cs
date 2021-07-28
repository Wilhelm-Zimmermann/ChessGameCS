using board;
using System;
using chess;
using System.Collections.Generic;

namespace Chess
{
    class Screen
    {
        public static void ShowGame(ChessGame game,bool[,] possiblePos)
        {
            ShowBoard(game.Board,possiblePos);
            Console.WriteLine();
            ShowCapturedPieces(game);
            Console.WriteLine();
            Console.WriteLine("Turn: " + game.Turn);
            Console.WriteLine();

            Console.WriteLine($"Player '{game.Player}'");
        }
        public static void ShowCapturedPieces(ChessGame game)
        {
            Console.WriteLine("Captured Pieces");
            Console.Write("White: ");
            ShowPieces(game.AllCapturedPieces(Color.White));
            Console.WriteLine();
            Console.Write("Black: ");
            ShowPieces(game.AllCapturedPieces(Color.Black));
        }

        public static void ShowPieces(List<Piece> pieces)
        {
            Console.Write("[");
            foreach(Piece p in pieces)
            {
                Console.Write(p + " ");
            }
            Console.Write("]");
        }
        public static void ShowBoard(Board board)
        {
            for (int i = 0; i < board.Rows; i++)
            {
                Console.Write(8 - i + " ");
                for (int c = 0; c < board.Columns; c++)
                {

                    PieceShow(board.GetPiece(i, c));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void ShowBoard(Board board, bool[,] posiblePos)
        {
            ConsoleColor originalBack = Console.BackgroundColor;
            ConsoleColor background = ConsoleColor.DarkBlue;
            for (int i = 0; i < board.Rows; i++)
            {
                Console.Write(8 - i + " ");
                for (int c = 0; c < board.Columns; c++)
                {
                    if (posiblePos[i, c])
                    {
                        Console.BackgroundColor = background;
                    }
                    else
                    {
                        Console.BackgroundColor = originalBack;
                    }
                    PieceShow(board.GetPiece(i, c));
                    Console.BackgroundColor = originalBack;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = originalBack;
        }
        public static void PieceShow(Piece piece)
        {

            if (piece == null)
            {
                Console.Write("- ");
            }
            else
            {

                if (piece.Color == Color.White)
                {
                    Console.Write(piece + " ");
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;

                    Console.Write(piece + " ");
                    Console.ForegroundColor = aux;
                }
            }
        }

        public static ChessPosition ReadPosition()
        {
            string s = Console.ReadLine();
            char column = s[0];
            int row = int.Parse(s[1] + "");
            return new ChessPosition(column, row);
        }
    }
}
