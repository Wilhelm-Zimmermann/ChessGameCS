using System;
using board;
using chess;

namespace Chess
{
    class Program
    {
        static void Main(string[] args)
        {
            ChessGame game = new ChessGame();
            while (!game.End)
            {
                try
                {
                    Console.Clear();
                    Screen.ShowBoard(game.Board);
                    Console.WriteLine();

                    Console.WriteLine("Turn: " + game.Turn);
                    Console.WriteLine();

                    Console.WriteLine($"Player '{game.Player}'");

                    Console.Write("Type origin pos: ");
                    Position origin = Screen.ReadPosition().ToPosition();
                    game.ValidateOriginPos(origin);
                    bool[,] posiblePos = game.Board.GetPiece(origin).PossibleMoves();

                    Console.Clear();
                    Console.WriteLine();
                    Screen.ShowBoard(game.Board, posiblePos);
                    Console.WriteLine();

                    Console.Write("Type destin pos: ");
                    Position destin = Screen.ReadPosition().ToPosition();
                    game.ValidateDestinPos(origin, destin);

                    game.RealizeMove(origin, destin);
                }
                catch (BoardExeption err)
                {
                    Console.WriteLine(err.Message);
                    Console.ReadLine();
                }
            }


        }
    }
}
