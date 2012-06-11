using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace View
{
    public class StdOutDisplay : IView
    {
        public void BoardUpdated(Color[,] board)
        {
             PrintBoard(board);
        }

        public void GameOver(int turns)
        {
            Console.WriteLine("Game Over. It took you {0} turns.", turns);
        }

        public void Init(Color[,] board)
        {
            PrintBoard(board);
        }

        public static void PrintBoard(Color[,] board)
        {
            int initCap = (board.GetLength(0) * board.GetLength(1)) * 2;
            StringBuilder sb = new StringBuilder(initCap);
            for (int y = 0; y < board.GetLength(0); y++)
            {
                for (int x = 0; x < board.GetLength(1); x++)
                {
                    sb.Append((char) ColorToLetter(board[y, x]));
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
