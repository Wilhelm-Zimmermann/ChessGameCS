using System;
using board;
using chess;

namespace Chess
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ChessGame game = new ChessGame();
                while (!game.End)
                {
                    Console.Clear();
                    Screen.ShowBoard(game.Board);
                    Console.Write("Type origin pos: ");
                    Position origin = Screen.ReadPosition().ToPosition();
                    Console.Write("Type destin pos: ");
                    Position destin = Screen.ReadPosition().ToPosition();

                    game.ExecuteMove(origin, destin);
                }
            }catch(BoardExeption err)
            {
                Console.WriteLine(err.Message);
            }

            
        }
    }
}
