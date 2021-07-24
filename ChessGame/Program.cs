using System;
using board;

namespace ChessGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(8, 8);

            Screen.ShowBoard(board);
        }
    }
}
