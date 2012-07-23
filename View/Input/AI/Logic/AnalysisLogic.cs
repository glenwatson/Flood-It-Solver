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
            bool[,] continuousVisited = new bool[board.Height(), board.Width()];
            bool[,] edgesVisited = new bool[board.Height(), board.Width()];
            int covered = EdgeCoverage(0, 0, continuousVisited, edgesVisited, board);
            return covered;
        }
        private int EdgeCoverage(int x, int y, bool[,] continuousVisited, bool[,] edgesVisited, Color[,] board)
        {
            continuousVisited.SetAt(x, y, true);
            Color thisColor = board.GetAt(x, y);
            int result = 0;
            if (board.CanGetLeft(x) && !continuousVisited.GetLeftOf(x, y))
            {
                if (thisColor == board.GetLeftOf(x, y))
                    result += EdgeCoverage(x - 1, y, continuousVisited, edgesVisited, board);
                else if (!edgesVisited.GetLeftOf(x, y))
                {
                    edgesVisited.SetLeftOf(x, y, true);
                    result++;
                }
            }
            if (board.CanGetAbove(y) && !continuousVisited.GetAboveOf(x, y))
            {
                if (thisColor == board.GetAboveOf(x, y))
                    result += EdgeCoverage(x, y - 1, continuousVisited, edgesVisited, board);
                else if (!edgesVisited.GetAboveOf(x, y))
                {
                    edgesVisited.SetAboveOf(x, y, true);
                    result++;
                }
            }
            if (board.CanGetRight(x) && !continuousVisited.GetRightOf(x, y))
            {
                if (thisColor == board.GetRightOf(x, y))
                    result += EdgeCoverage(x + 1, y, continuousVisited, edgesVisited, board);
                else if (!edgesVisited.GetRightOf(x, y))
                {
                    edgesVisited.SetRightOf(x, y, true);
                    result++;
                }
            }
            if (board.CanGetBelow(y) && !continuousVisited.GetBelowOf(x, y))
            {
                if (thisColor == board.GetBelowOf(x, y))
                    result += EdgeCoverage(x, y + 1, continuousVisited, edgesVisited, board);
                else if (!edgesVisited.GetBelowOf(x, y))
                {
                    edgesVisited.SetBelowOf(x, y, true);
                    result++;
                }
            }
            return result;
        }
    }
}
