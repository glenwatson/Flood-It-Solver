using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using View.Input.AI.Logic.MapModel;
using View.Input.AI.Logic.Moves;

namespace View.Input.AI.Logic
{
    public class IncreaseSurfaceAreaMapLogic : AILogic
    {
        private int _lookAheadLevel;
        public IncreaseSurfaceAreaMapLogic(int lookAheadLevel)
        {
            _lookAheadLevel = lookAheadLevel;
        }

        public override SuggestedMoves ChooseColor(Color[,] board)
        {
            MapNode head = Builder.BuildMap(board);

            MapNodeDecisionTree decisionTree = BuildDecisionTree(head.Clone(), _lookAheadLevel);
            Stack<Color> currentDecisionTreePath = new Stack<Color>();
            SurfaceAreaResult bestSurfaceArea = GetBest(decisionTree, new SurfaceAreaResult { SurfaceArea = 0, Path = new Color[0] }, currentDecisionTreePath);

            SuggestedMoves moves = new SuggestedMoves();
            bestSurfaceArea.Path.ToList().ForEach(c => moves.AddLast(new SuggestedMove(c)));
            return moves;

        }
        class SurfaceAreaResult
        {
            public int SurfaceArea { get; set; }
            public Color[] Path { get; set; }
        }
        private SurfaceAreaResult GetBest(MapNodeDecisionTree currentDecisionTree, SurfaceAreaResult bestSurfaceArea, Stack<Color> highestColorStack)
        {
            highestColorStack.Push(currentDecisionTree.Color); //mirror the execution stack
            if (GetSurfaceArea(currentDecisionTree.CurrentMap) > bestSurfaceArea.SurfaceArea)
            {
                bestSurfaceArea.SurfaceArea = GetSurfaceArea(currentDecisionTree.CurrentMap);
                bestSurfaceArea.Path = highestColorStack.ToArray();
            }
            foreach (MapNodeDecisionTree decisionTreeChild in currentDecisionTree.Children)
            {
                SurfaceAreaResult childSurfaceArea = GetBest(decisionTreeChild, bestSurfaceArea, highestColorStack);
                if (childSurfaceArea.SurfaceArea > bestSurfaceArea.SurfaceArea)
                {
                    bestSurfaceArea = childSurfaceArea;
                }
            }
            highestColorStack.Pop(); //mirror the execution stack
            return bestSurfaceArea;
        }

        //public override SuggestedMoves ChooseColor(Color[,] board)
        //{
        //    MapNode head = Builder.BuildMap(board);
        //    ISet<MapNode> neighbors = head.GetNeighbors();

        //    int highestSurfaceArea = int.MinValue;
        //    Color highestSurfaceAreaColor = head.Color;

        //    MapNode headClone = head.Clone();
        //    foreach (MapNode neighbor in neighbors)
        //    {
        //        int neighborsIncreaseSurrfaceArea = neighbor.GetNeighbors().Count;
        //        if (neighborsIncreaseSurrfaceArea > highestSurfaceArea)
        //        {
        //            highestSurfaceArea = neighborsIncreaseSurrfaceArea;
        //            highestSurfaceAreaColor = neighbor.Color;
        //        }
        //    }

        //    return new SuggestedMoves(highestSurfaceAreaColor);
        //}


        class MapNodeDecisionTree
        {
            public MapNode CurrentMap { get; private set; }
            public Color Color { get { return CurrentMap.Color; } }
            public List<MapNodeDecisionTree> Children { get; private set; }
            public MapNodeDecisionTree(MapNode currentMap)
            {
                CurrentMap = currentMap;
                Children = new List<MapNodeDecisionTree>();
            }
        }
        private MapNodeDecisionTree BuildDecisionTree(MapNode head, int recurseLevel)
        {
            MapNodeDecisionTree decisionNode = new MapNodeDecisionTree(head);
            if (recurseLevel > 0)
            {
                foreach (MapNode neighbor in head.GetNeighbors().Distinct(new MapNodeColorComparer()))
                {
                    MapNode headClone = head.Clone();
                    headClone.PickColor(neighbor.Color); //make the move
                    MapNodeDecisionTree childNode = BuildDecisionTree(headClone, recurseLevel - 1);
                    decisionNode.Children.Add(childNode);
                }
            }
            return decisionNode;
        }

        private int GetSurfaceArea(MapNode head)
        {
            return head.GetNeighbors().Count;
        }
        class MapNodeColorComparer : IEqualityComparer<MapNode>
        {
            public bool Equals(MapNode x, MapNode y)
            {
                return x.Color == y.Color;
            }

            public int GetHashCode(MapNode obj)
            {
                return (int)obj.Color;
            }
        }
    }
}
