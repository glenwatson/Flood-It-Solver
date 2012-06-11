using System;
using Model;

namespace View
{
    public class AIPlayer : IInput
    {
        private readonly AILogic _logic;

        public static AIPlayer WithRandomLogic()
        {
            return new AIPlayer(new RandomLogic());
        }

        public static AIPlayer WithAnalysisLogic()
        {
            return new AIPlayer(new AnalysisLogic());
        }
        
        private AIPlayer(AILogic logic)
        {
            _logic = logic;
        }

        public Color MakeMove(Color[,] board)
        {
            return _logic.ChooseColor(board);
        }

        abstract class AILogic
        {
            public abstract Color ChooseColor(Color[,] board);
        }

        class RandomLogic : AILogic
        {
            Random rand = new Random();
            public override Color ChooseColor(Color[,] board)
            {
                var randInt = rand.Next(0, Enum.GetValues(typeof(Color)).Length);
                var ans = (Color)randInt;
                return ans;
            }
        }

        class AnalysisLogic : AILogic
        {
            public override Color ChooseColor(Color[,] board)
            {
                //TODO: analyse the board and choose a color
                Color bestColor = Color.Red;
                int greatestSurfaceArea = 0;
                foreach (var color in board)
                {
                    Board boardLogic = new Board(board);
                    boardLogic.Pick(color);
                    int surfaceArea = EdgeCoverage(boardLogic.Colors);
                    if (surfaceArea > greatestSurfaceArea)
                    {
                        greatestSurfaceArea = surfaceArea;
                        bestColor = color;
                    }
                }
                return bestColor;
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

        public event Player.ColorSelectedDel ColorSelected;
        private Player player;
        public void SetPlayer(Player p)
        {
            player = p;
        }
    }
}
