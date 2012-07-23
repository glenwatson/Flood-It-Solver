using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using View.Extentions;

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
            bool[,] visited = new bool[board.Height(), board.Width()];
            int covered = EdgeCoverage(0, 0, visited, board);
            return covered;
        }
        private int EdgeCoverage(int x, int y, bool[,] visited, Color[,] board)
        {
            visited[x, y] = true;
            Color thisColor = board[y, x];
            int result = 0;
            if (board.CanGetLeft(x) && !visited[x - 1, y])
            {
                if (thisColor == board.GetLeftOf(x, y))
                    result += EdgeCoverage(x - 1, y, visited, board);
                else
                    result++;
            }
            if (board.CanGetAbove(y) && !visited[x, y - 1])
            {
                if (thisColor == board.GetAboveOf(x, y))
                    result += EdgeCoverage(x, y - 1, visited, board);
                else
                    result++;
            }
            if (board.CanGetRight(x) && !visited[x + 1, y])
            {
                if (thisColor == board.GetRightOf(x, y))
                    result += EdgeCoverage(x + 1, y, visited, board);
                else
                    result++;
            }
            if (board.CanGetBelow(y) && !visited[x, y + 1])
            {
                if (thisColor == board.GetBelowOf(x, y))
                    result += EdgeCoverage(x, y + 1, visited, board);
                else
                    result++;
            }
            return result;
        }
    }
}
