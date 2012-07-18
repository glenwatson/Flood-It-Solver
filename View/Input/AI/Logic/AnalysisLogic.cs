using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace View.Input.AI.Logic
{
    class AnalysisLogic : AILogic
    {
        public override SuggestedMoves ChooseColor(Color[,] board)
        {
            //TODO: analyse the board and choose a color
            Color bestColor = Color.Red;
            int greatestSurfaceArea = 0;
            foreach (Object colorObj in Enum.GetValues(typeof(Color)))
            {
                Color color = (Color)colorObj;
                Board boardLogic = new Board(board);
                boardLogic.Pick(color);
                int surfaceArea = EdgeCoverage(boardLogic.GetCopyOfBoard());
                if (surfaceArea > greatestSurfaceArea)
                {
                    greatestSurfaceArea = surfaceArea;
                    bestColor = color;
                }
            }
            return new SuggestedMoves ( bestColor );
        }
        private int EdgeCoverage(Color[,] board)
        {
            bool[,] visited = new bool[board.GetLength(0), board.GetLength(1)];
            int covered = EdgeCoverage(0, 0, visited, board);
            return covered;
        }
        private int EdgeCoverage(int y, int x, bool[,] visited, Color[,] board)
        {
            visited[y, x] = true;
            Color thisColor = board[y, x];
            int result = 0;
            if (x - 1 >= 0 && !visited[y, x - 1])
            {
                if (thisColor == board[y, x - 1])
                    result += EdgeCoverage(y, x - 1, visited, board);
                else
                    result++;
            }
            if (y - 1 >= 0 && !visited[y - 1, x])
            {
                if (thisColor == board[y - 1, x])
                    result += EdgeCoverage(y - 1, x, visited, board);
                else
                    result++;
            }
            if (x + 1 < board.GetLength(1) && !visited[y, x + 1])
            {
                if (thisColor == board[y, x + 1])
                    result += EdgeCoverage(y, x + 1, visited, board);
                else
                    result++;
            }
            if (y + 1 < board.GetLength(0) && !visited[y + 1, x])
            {
                if (thisColor == board[y + 1, x])
                    result += EdgeCoverage(y + 1, x, visited, board);
                else
                    result++;
            }
            return result;
        }
    }
}
