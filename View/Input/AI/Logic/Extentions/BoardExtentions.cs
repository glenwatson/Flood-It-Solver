using Model;

namespace View.Input.AI.Logic.Extentions
{
    static class BoardExtentions
    {
        public static int Height(this Color[,] board)
        {
            return board.GetLength(0);
        }
        public static int Width(this Color[,] board)
        {
            return board.GetLength(1);
        }

        public static bool CanGetLeft(this Color[,] board, int x)
        {
            return x > 0;
        }
        public static bool CanGetRight(this Color[,] board, int x)
        {
            return x < board.Width()-1;
        }
        public static bool CanGetAbove(this Color[,] board, int y)
        {
            return y > 0;
        }
        public static bool CanGetBelow(this Color[,] board, int y)
        {
            return y < board.Height() - 1;
        }

        public static Color GetColorAt(this Color[,] board, int x, int y)
        {
            return board[x, y];
        }
        public static Color GetLeftOf(this Color[,] board, int x, int y)
        {
            return board[x-1, y];
        }
        public static Color GetRightOf(this Color[,] board, int x, int y)
        {
            return board[x + 1, y];
        }
        public static Color GetAboveOf(this Color[,] board, int x, int y)
        {
            return board[x, y-1];
        }
        public static Color GetBelowOf(this Color[,] board, int x, int y)
        {
            return board[x, y+1];
        }
    }
}
