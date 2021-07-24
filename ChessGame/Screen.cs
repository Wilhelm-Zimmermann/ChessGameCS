using board;
namespace ChessGame
{
    class Screen
    {
        public static void ShowBoard(Board board)
        {
            for (int i = 0; i < board.Rows; i++)
            {
                for (int c = 0; c < board.Columns; c++)
                {
                    if(board.GetPiece(i,c) == null)
                    {
                        System.Console.Write("- ");
                    }
                    else
                    {

                        System.Console.Write(board.GetPiece(i,c) + " ");
                    }
                }
                System.Console.WriteLine();
            }
        }
    }
}
