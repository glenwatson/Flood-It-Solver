using System;
using System.Text;
using Model;
using View.Extentions;
using View.Shared;

namespace View.Views
{
    public class StdOutDisplay : IView
    {
        #region IView
        public void Display() {}
        public void BoardUpdated(Color[,] board)
        {
             PrintBoard(board);
        }

        public void GameOver(WinEventArgs e)
        {
            Console.WriteLine("Game Over. It took you {0} turns.", e.Turns);
        }
        #endregion

        public void Init(Color[,] board)
        {
            PrintBoard(board);
        }

        private static void PrintBoard(Color[,] board)
        {
            int estimatedLength = (board.GetLength(0) * board.GetLength(1)) * 2;
            StringBuilder sb = new StringBuilder(estimatedLength);
            for (int y = 0; y < board.Height(); y++)
            {
                for (int x = 0; x < board.Width(); x++)
                {
                    sb.Append((char) ColorToLetter(board.GetAt(x, y)));
                    sb.Append(' ');
                }
                sb.AppendLine();
            }
            Console.WriteLine(sb);
        }

        private static char ColorToLetter(Color color)
        {
            char result = 'x';
            switch (color)
            {
                case Color.Red:
                    result = 'R';
                    break;
                case Color.Orange:
                    result = 'O';
                    break;
                case Color.Yellow:
                    result = 'Y';
                    break;
                case Color.Green:
                    result = 'G';
                    break;
                case Color.Blue:
                    result = 'B';
                    break;
                case Color.Purple:
                    result = 'P';
                    break;
            }
            return result;
        }
        
    }
}
